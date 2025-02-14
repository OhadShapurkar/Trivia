#pragma once

#include <iostream>
#include "Room.h"
#include "Helper.h"

using namespace std;

class Game;

class User
{
public:
	User(string username, SOCKET sock);//create user by his usernaem and socket
	void send(string message);//send this user message using socket
	string getUsername();//return string of this username
	SOCKET getSocket();//return socket of this user
	Room* getRoom();//return room of this user if exist
	Game* getGame();//return game of this user if exist
	void setGame(Game* gm);//set this user game if he started any game
	void clearGame();//clear this user's game
	void clearRoom();//clear this users room
	bool createRoom(int id, string roomName, int maxUsers, int questionNum, int questionTime);//create new room by id roomname max players question Number and question time
	bool joinRoom(Room* newRoom);//adding player to a room - newRoom
	void leaveRoom();//delete this user from his room
	int closeRoom();//delete all players from this room if this user is admin
	bool leaveGame();//delete this user from his room and returns true if succes else false

private:
	string _username;
	Room* _currRoom;
	Game* _currGame;
	SOCKET _sock;
};