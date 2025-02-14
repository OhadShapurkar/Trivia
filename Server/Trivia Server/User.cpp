#include "User.h"
#include "Game.h"

User::User(string username, SOCKET sock)
{
	this->_username = username;
	this->_sock = sock;
	this->_currRoom = nullptr;
	this->_currGame = nullptr;
}

void User::send(string message)
{
	Helper::sendData(this->_sock, message);
}

string User::getUsername()
{
	return this->_username;
}

SOCKET User::getSocket()
{
	return this->_sock;
}

Room* User::getRoom()
{
	return this->_currRoom;
}

Game* User::getGame()
{
	return this->_currGame;
}

void User::setGame(Game* gm)
{
	this->_currGame = gm;
}

void User::clearGame()
{
	this->_currGame = nullptr;
}

void User::clearRoom()
{
	this->_currRoom = nullptr;
}

bool User::createRoom(int id, string roomName, int maxUsers, int questionNum, int questionTime)
{
	if (this->_currRoom != nullptr)
	{
		Helper::sendData(this->_sock, "1141");
		return false;
	}
	else
	{
		_currRoom = new Room(id, this, roomName, maxUsers, questionNum, questionTime);
		Helper::sendData(this->_sock, "1140");
		return true;
	}
}

bool User::joinRoom(Room* newRoom)
{
	if (this->_currRoom == nullptr)
	{
		if (newRoom->JoinRoom(this))
		{
			this->_currRoom = newRoom;
			return true;
		}
		return false;
	}
	else
	{
		return false;
	}
}

void User::leaveRoom()
{
	if (this->_currRoom != nullptr)
	{
		this->_currRoom->leaveRoom(this);
		this->_currRoom = nullptr;
	}
}

int User::closeRoom()
{
	if (_currRoom != nullptr)
	{
		int id = _currRoom->closeRoom(this);
		if (id != -1)
		{
			delete _currRoom;
			this->_currRoom = nullptr;
		}
		return id;
	}
	return -1;
}

bool User::leaveGame()
{
	if (this->_currGame != nullptr)
	{
		this->_currGame->leaveGame(this);
		if (this->_currGame == nullptr)
		{
			return false;
		}
		this->_currGame = nullptr;
		return true;
	}
	return false;
}