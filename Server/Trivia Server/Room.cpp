#include "Room.h"
#include "User.h"

Room::Room(int id, User* admin, string roomName, int maxUsers, int questionNum, int questionTime)
{
	this->_id = id;
	this->_admin = admin;
	this->_name = roomName;
	this->_maxUsers = maxUsers;
	this->_questionNum = questionNum;
	this->_questionTime = questionTime;
	this->_users.push_back(admin);
}

bool Room::JoinRoom(User* user)
{
	if (this->_users.size() < this->_maxUsers)
	{
		this->_users.push_back(user);
		user->send(("1100" + Helper::getPaddedNumber(getQuestionNum(), 2) + Helper::getPaddedNumber(getQuestionTime(), 2)));
		this->sendMessage(this->getUsersListMessage());
		return true;
	}
	else
	{
		user->send("1101");
		return false;
	}
}

void Room::leaveRoom(User* user)
{
	vector<User*>::iterator it;
	it = find(this->_users.begin(), this->_users.end(), user);
	if (it != this->_users.end())
	{
		this->_users.erase(it);
		user->send("1120");
		sendMessage(getUsersListMessage());
	}
	
}

int Room::closeRoom(User* user)
{
	if (user == this->_admin)
	{
		for (int i = 0; i < this->_users.size(); i++)
		{
			this->_users[i]->send("116");
			if (this->_users[i] != this->_admin)
			{
				this->_users[i]->clearRoom();
			}
		}
		return this->_id;
	}
	return -1;
}

vector<User*> Room::getUsers()
{
	return this->_users;
}

string Room::getUsersListMessage()
{
	string UserList = "108";

	UserList += to_string(this->_users.size());

	for (int i = 0; i < this->_users.size(); i++)
	{
		UserList += Helper::getPaddedNumber(this->_users[i]->getUsername().length(), 2) + this->_users[i]->getUsername();
	}
	return UserList;
}

int Room::getQuestionNum()
{
	return this->_questionNum;
}

int Room::getQuestionTime()
{
	return _questionTime;
}

int Room::getid()
{
	return this->_id;
}

string Room::getName()
{
	return this->_name;
}

string Room::getUsersAsString(vector<User*> Users, User* excludeUser)
{
	string allUsers;
	for (int i = 0; i < Users.size(); i++)
	{
		if (Users[i] != excludeUser)
		{
			allUsers += Users[i]->getUsername();
			allUsers += " ";
		}
	}
	return allUsers;
}

void Room::sendMessage(User* excludeUser, string message)
{
	for (int i = 0; i < this->_users.size();  i++)
	{
		if (this->_users[i] != excludeUser)
		{
			try
			{
				this->_users[i]->send(message);
			}
			catch (exception& ex)
			{
				cout << ex.what() << endl;
			}
		}
	}
}

void Room::sendMessage(string message)
{
	this->sendMessage(NULL, message);
}