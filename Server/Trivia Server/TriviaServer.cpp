#include "TriviaServer.h"

TriviaServer::TriviaServer()
{
	//_db = DataBase();
	_sock = ::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (_sock == INVALID_SOCKET)
	{
		throw exception("INVALID SOCKET");
	}
}
TriviaServer::~TriviaServer()
{
	::closesocket(_sock);
}

void TriviaServer::serve()
{
	bindAndListen();

	thread t(&TriviaServer::handleRecievedMesssages, this);
	t.detach();

	while (true)
	{
		accept();
	}
}

void TriviaServer::bindAndListen()
{
	struct sockaddr_in sa = { 0 };
	sa.sin_port = htons(PORT);
	sa.sin_family = AF_INET;
	sa.sin_addr.s_addr = INADDR_ANY;

	if (::bind(_sock, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
	{
		throw exception("EXCEPTION: SOCKET BIND");
	}

	if (::listen(_sock, SOMAXCONN) == SOCKET_ERROR)
	{
		throw exception("EXCEPTION: SOCKET LISTEN");
	}
}

void TriviaServer::accept()
{
	SOCKET client_socket = ::accept(_sock, NULL, NULL);
	if (client_socket == INVALID_SOCKET)
	{
		throw exception("EXCEPTION: INVALID SOCKET");
	}

	cout << "Accepted new client: " << client_socket << endl;
	thread t(&TriviaServer::clientHandler, this, client_socket);
	t.detach();
}

void TriviaServer::clientHandler(SOCKET client_socket)
{
	ReceivedMessage* msg = nullptr;
	try
	{
		int msgCode = Helper::getMessageTypeCode(client_socket);
		cout << "Received message code " << msgCode << " from " << client_socket << endl;
		while (msgCode != 299)
		{
			msg = buildRecieveMessage(client_socket, msgCode);
			addRecievedMessages(msg);
			msgCode = Helper::getMessageTypeCode(client_socket);
		}
		msg = buildRecieveMessage(client_socket, 299);
		addRecievedMessages(msg);
	}
	catch(exception e)
	{
		cout << e.what() << endl;
		msg = buildRecieveMessage(client_socket, 299);
		addRecievedMessages(msg);
	}
	closesocket(client_socket);
}

void TriviaServer::addRecievedMessages(ReceivedMessage* msg)
{
	{
		lock_guard<mutex> lock(_mtxRecievedMessages);
		_queRcvMessages.push(msg);
	}
	_cvMsgQue.notify_all();
}

ReceivedMessage* TriviaServer::buildRecieveMessage(SOCKET client_socket, int msgCode)
{
	ReceivedMessage* msg = nullptr;
	vector<string> values;
	switch (msgCode)
	{
		case 200:
		{
			int usernameLength = Helper::getIntPartFromSocket(client_socket, 2);
			string username = Helper::getStringPartFromSocket(client_socket, usernameLength);
			int passwordLength = Helper::getIntPartFromSocket(client_socket, 2);
			string password = Helper::getStringPartFromSocket(client_socket, passwordLength);
			values.push_back(username);
			values.push_back(password);
			break;
		}
		case 203:
		{
			int usernameLength = Helper::getIntPartFromSocket(client_socket, 2);
			string username = Helper::getStringPartFromSocket(client_socket, usernameLength);
			int passwordLength = Helper::getIntPartFromSocket(client_socket, 2);
			string password = Helper::getStringPartFromSocket(client_socket, passwordLength);
			int emailLength = Helper::getIntPartFromSocket(client_socket, 2);
			string email = Helper::getStringPartFromSocket(client_socket, emailLength);
			values.push_back(username);
			values.push_back(password);
			values.push_back(email);
			break;
		}
		case 207:
			values.push_back(Helper::getStringPartFromSocket(client_socket, 4));
			break;
		case 209:
			values.push_back(Helper::getStringPartFromSocket(client_socket, 4));
			break;
		case 213:
		{
			int roomNameLength = Helper::getIntPartFromSocket(client_socket, 2);
			string roomName = Helper::getStringPartFromSocket(client_socket, roomNameLength);
			string playersNumber = Helper::getStringPartFromSocket(client_socket, 1);
			string questionsNumber = Helper::getStringPartFromSocket(client_socket, 2);
			string questionTimeInSec = Helper::getStringPartFromSocket(client_socket, 2);
			values.push_back(roomName);
			values.push_back(playersNumber);
			values.push_back(questionsNumber);
			values.push_back(questionTimeInSec);
			break;
		}
		case 219:
		{
			string answerNumber = Helper::getStringPartFromSocket(client_socket, 1);
			string answerTime = Helper::getStringPartFromSocket(client_socket, 2);
			values.push_back(answerNumber);
			values.push_back(answerTime);
			break;
		}
		default:
			msg = new ReceivedMessage(client_socket, msgCode);
			return msg;
	}
	msg = new ReceivedMessage(client_socket, msgCode, values);
	return msg;
}

void TriviaServer::handleRecievedMesssages()
{
	int msgCode = NULL;
	SOCKET client_socket = NULL;
	while (true)
	{
		unique_lock<mutex> lck(_mtxRecievedMessages);

		if (_queRcvMessages.empty())
		{
			_cvMsgQue.wait(lck);
		}

		if (_queRcvMessages.empty())
		{
			continue;
		}

		ReceivedMessage* currMessage = _queRcvMessages.front();
		_queRcvMessages.pop();
		lck.unlock();
		cout << "------------------------------" << endl;
		cout << "Handling message " << currMessage->getMessageCode() << " from " << currMessage->getSock() << endl;

		try
		{
			client_socket = currMessage->getSock();
			msgCode = currMessage->getMessageCode();
			currMessage->setUser(getUserBySocket(client_socket));

			switch (msgCode)
			{
			case 200:
				handleSignin(currMessage); // works
				break;
			case 201:
				handleSignout(currMessage); // works
				break;
			case 203:
				handleSignup(currMessage); // works
				break;
			case 205:
				handleGetRooms(currMessage); // works
				break;
			case 207:
				handleGetUserInRoom(currMessage); // works
				break;
			case 209:
				handleJoinRoom(currMessage); // works
				break;
			case 211:
				handleLeaveRoom(currMessage); // works
				
				break;
			case 213:
				handleCreateRoom(currMessage); // works
				break;
			case 215:
				handleCloseRoom(currMessage); // works
				break;
			case 217:
				handleStartGame(currMessage);
				break;
			case 219:
				handlePlayerAnswer(currMessage);
				break;
			case 222:
				handleLeaveGame(currMessage);
				break;
			case 223:
				handleGetBestScores(currMessage);
				break;
			case 225:
				handleGetPersonalStatus(currMessage);
				break;
			case 299:
				safeDeleteUser(currMessage);
				break;
			default:
				safeDeleteUser(currMessage);
				// send to handle functions...
			}

			delete currMessage;
		}
		catch (...)
		{
			safeDeleteUser(currMessage);
		}
	}
}

void TriviaServer::safeDeleteUser(ReceivedMessage* msg)
{
	try
	{
		handleSignout(msg);
		closesocket(msg->getSock());
	}
	catch(...){}
}

void TriviaServer::handleSignout(ReceivedMessage* msg)
{
	if (msg->getUser()!=nullptr)
	{
		_connectedUsers.erase(msg->getSock());
		// handle close room
		// handle leave room
		// handle leave game
	}
}

bool TriviaServer::handleCreateRoom(ReceivedMessage* msg)
{
	if (msg->getUser() == nullptr)
	{
		return false;
	}
	vector<string>& values = msg->getValues();
	_roomIdSequence++;
	if (msg->getUser()->createRoom(_roomIdSequence, values[0], stoi(values[1]), stoi(values[2]), stoi(values[3])))
	{
		_roomList[_roomIdSequence] = msg->getUser()->getRoom();
		return true;
	}
	return false;
}

bool TriviaServer::handleCloseRoom(ReceivedMessage* msg)
{
	if (msg->getUser()->getRoom() != nullptr)
	{
		int id = msg->getUser()->closeRoom();
		if (id != -1)
		{
			_roomList.erase(id);
			return true;
		}
	}
	return false;
}

bool TriviaServer::handleSignup(ReceivedMessage* msg)
{
	vector<string>& values = msg->getValues();
	if (!Validator::isPasswordValid(values[1]))
	{
		Helper::sendData(msg->getSock(), "1041");
		return false;
	}
	if (!Validator::isUsernameValid(values[0]))
	{
		Helper::sendData(msg->getSock(), "1043");
		return false;
	}
	if (_db.isUserExists(values[0]))
	{
		Helper::sendData(msg->getSock(), "1042");
		return false;
	}
	if (!_db.addNewUser(values[0], values[1], values[2]))
	{
		Helper::sendData(msg->getSock(), "1044");
		return false;
	}
	Helper::sendData(msg->getSock(), "1040");
	return true;
}

User* TriviaServer::handleSignin(ReceivedMessage* msg)
{
	User* newUser;
	string username = msg->getValues()[0];
	string password = msg->getValues()[1];
	if (_db.isUserAndPassMatch(username, password))
	{
		if (getUserByName(username) != nullptr)
		{
			// user already connected..
			Helper::sendData(msg->getSock(), "1022");
			return nullptr;
		}
		// add user
		Helper::sendData(msg->getSock(), "1020");
		newUser = new User(username, msg->getSock());
		_connectedUsers[msg->getSock()] = newUser;
		return newUser;
	}
	// username and password don't match
	Helper::sendData(msg->getSock(), "1021");
	return nullptr;
}

Room* TriviaServer::getRoomById(int id)
{
	map<int, Room*>::iterator it = _roomList.find(id);
	if (it == _roomList.end())
	{
		return nullptr;
	}
	return it->second;
}

User* TriviaServer::getUserByName(string name)
{
	for (map<SOCKET, User*>::iterator it = _connectedUsers.begin(); it != _connectedUsers.end(); ++it)
	{
		if (it->second->getUsername() == name)
		{
			return it->second;
		}
	}
	return nullptr;
}

User* TriviaServer::getUserBySocket(SOCKET sock)
{
	for (map<SOCKET, User*>::iterator it = _connectedUsers.begin(); it != _connectedUsers.end(); ++it)
	{
		if (it->first == sock)
		{
			return it->second;
		}
	}
	return nullptr;
}

bool TriviaServer::handleJoinRoom(ReceivedMessage* msg)
{
	if (msg->getUser() != nullptr)
	{
		Room* room = getRoomById(stoi(msg->getValues()[0]));
		if (room != nullptr)
		{
			msg->getUser()->joinRoom(room);
			return true;
		}
	}

	Helper::sendData(msg->getSock(), "1102");
	return false;
}

bool TriviaServer::handleLeaveRoom(ReceivedMessage* msg)
{
	if (msg->getUser() != nullptr)
	{
		if (msg->getUser()->getRoom() != nullptr)
		{
			msg->getUser()->leaveRoom();
			msg->getUser()->send("1120");
			return true;
		}
	}
	return false;
}

void TriviaServer::handleGetUserInRoom(ReceivedMessage* msg)
{
	Room* room = getRoomById(stoi(msg->getValues()[0]));
	if (room != nullptr)
	{
		Helper::sendData(msg->getSock(), room->getUsersListMessage());
		return;
	}
	Helper::sendData(msg->getSock(), "1080");
}	

void TriviaServer::handleGetRooms(ReceivedMessage* msg)
{
	string str = "106" + Helper::getPaddedNumber(this->_roomList.size(), 4);
	for (map<int, Room*>::iterator it = this->_roomList.begin(); it != this->_roomList.end(); ++it)
	{
		str += Helper::getPaddedNumber(it->second->getid(), 4) + Helper::getPaddedNumber(it->second->getName().length(), 2) + it->second->getName();
	}
	 Helper::sendData(msg->getSock(), str);
}

void TriviaServer::handleLeaveGame(ReceivedMessage* msg)
{
	if (msg->getUser()->leaveGame())
	{
		delete msg->getUser()->getGame();
	}
}	

 void TriviaServer::handleStartGame(ReceivedMessage* msg)
{
	try
	{
		Game* newGame = new Game(msg->getUser()->getRoom()->getUsers(), msg->getUser()->getRoom()->getQuestionNum(), _db);
		_roomList.erase(msg->getUser()->getRoom()->getid());

		newGame->sendFirstQuestion();
	}
	catch (exception ex)
	{
	}
}


void TriviaServer::handlePlayerAnswer(ReceivedMessage* msg)
{
	if (msg->getUser()->getGame() != nullptr)
	{
		cout << msg->getValues()[0] << "   " << msg->getValues()[1] << endl;
		if (!(msg->getUser()->getGame()->handleAnswerFromUser(msg->getUser(), stoi(msg->getValues()[0]), stoi(msg->getValues()[1]))))
		{
			cout << "deleting game and closing room" << endl;
			msg->getUser()->getGame()->~Game();
			msg->getUser()->closeRoom();
		}
	}
}

void TriviaServer::handleGetBestScores(ReceivedMessage* msg)
{
	string bestScores = _db.getBestScores();
	Helper::sendData(msg->getSock(), bestScores);
}

void TriviaServer::handleGetPersonalStatus(ReceivedMessage* msg)
{
	string personalStatus = _db.getPersonalStatus(msg->getUser()->getUsername());
	
	
	Helper::sendData(msg->getSock(), "126" + personalStatus);
}