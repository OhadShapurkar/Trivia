#pragma once
#include <string>

using namespace std;

class Validator
{
public:
	static bool isPasswordValid(string password);/*return true if the password that sent from a user is correct else returns false 
	user must have password with 4 digits no spaces, one digit, one upper case letter and one lower case letter*/
	static bool isUsernameValid(string username);/*return true if the usernaem that sent from a user is correct else returns false
	username must start with a letter without spaces and not empty*/
};