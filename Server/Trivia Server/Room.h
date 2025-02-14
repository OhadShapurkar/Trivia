#pragma once
#include <vector>
#include <iostream>
#include <vector>
#include <string>

using namespace std;

class User;

class Room
{
public:
	Room(int id, User* admin, string roomName, int maxUsers, int questionNum, int questionTime); //create Room by id, the user that craeted the room, room name, question number and time per each question
	bool JoinRoom(User* user); //add player to this room
	void leaveRoom(User* user);//remove player from this room
	int closeRoom(User* user);//remove all players from this room
	vector<User*> getUsers();//return vector of all users in this room
	string getUsersListMessage();//return string of all users in this room
	int getQuestionTime();//return time per each question
	int getQuestionNum();//return questions number
	int getid();//return room id
	string getName();//return room name

private:
	vector<User*> _users;
	User* _admin;
	int _maxUsers;
	int _questionTime;
	int _questionNum;
	string _name;
	int _id;

	string getUsersAsString(vector<User*> Users, User* excludeUser);
	void sendMessage(User* excludeUser, string message);
	void sendMessage(string message);
};