#pragma once

#include <iostream>
#include <map>
#include <mutex>
#include <queue>
#include <condition_variable>
#include <exception>
#include <thread>
#include "Game.h"
#include "DataBase.h"
#include "Room.h"
#include "User.h"
#include "ReceivedMessage.h"
#include "Validator.h"

#define PORT 8820

using namespace std;

class TriviaServer
{
public:
	TriviaServer();
	~TriviaServer();
	void serve();

private:
	SOCKET _sock;
	map<SOCKET, User*> _connectedUsers;
	DataBase _db;
	map<int, Room*> _roomList;
	mutex _mtxRecievedMessages;
	queue <ReceivedMessage*> _queRcvMessages;
	condition_variable _cvMsgQue;
	int _roomIdSequence;

	void bindAndListen();
	void accept();
	void clientHandler(SOCKET client_socket);
	void safeDeleteUser(ReceivedMessage* msg);
	User* handleSignin(ReceivedMessage* msg);
	bool handleSignup(ReceivedMessage* msg);
	void handleSignout(ReceivedMessage* msg);
	void handleLeaveGame(ReceivedMessage* msg);
	void handleStartGame(ReceivedMessage* msg);
	void handlePlayerAnswer(ReceivedMessage* msg);
	bool handleCreateRoom(ReceivedMessage* msg);
	bool handleCloseRoom(ReceivedMessage* msg);
	bool handleJoinRoom(ReceivedMessage* msg);
	bool handleLeaveRoom(ReceivedMessage* msg);
	void handleGetUserInRoom(ReceivedMessage* msg);
	void handleGetRooms(ReceivedMessage* msg);
	void handleGetBestScores(ReceivedMessage* msg);
	void handleGetPersonalStatus(ReceivedMessage* msg);
	void handleRecievedMesssages();
	void addRecievedMessages(ReceivedMessage* msg);
	ReceivedMessage* buildRecieveMessage(SOCKET client_socket, int msgCode);
	User* getUserByName(string name);
	User* getUserBySocket(SOCKET sock);
	Room* getRoomById(int id);
};