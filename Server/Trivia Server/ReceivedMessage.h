#pragma once
#include <string>
#include <vector>
#include "User.h"

using namespace std;

class ReceivedMessage
{
public:
	ReceivedMessage(SOCKET sock, int messageCode);//build a message from client by socket and message code
	ReceivedMessage(SOCKET sock, int messageCode, vector<string> values);//build a message from client by socket and message code and values from this message
	SOCKET getSock();//return socket of this message
	User* getUser();//return the user that have send this message
	void setUser(User* user);//set user that send this message
	int getMessageCode();//return this message code
	vector<string>& getValues();//return this values from the message
private:
	SOCKET _sock;
	User* _user;
	int _messageCode;
	vector<string> _values;
};
