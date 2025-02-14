#include "ReceivedMessage.h"

ReceivedMessage::ReceivedMessage(SOCKET sock, int messageCode) : _sock(sock), _messageCode(messageCode)
{
}

ReceivedMessage::ReceivedMessage(SOCKET sock, int messageCode, vector<string> values) : _sock(sock), _messageCode(messageCode), _values(values)
{
}

SOCKET ReceivedMessage::getSock()
{
	return _sock;
}

User* ReceivedMessage::getUser()
{
	return _user;
}

void ReceivedMessage::setUser(User* user)
{
	_user = user;
}

int ReceivedMessage::getMessageCode()
{
	return _messageCode;
}

vector<string>& ReceivedMessage::getValues()
{
	return _values;
}