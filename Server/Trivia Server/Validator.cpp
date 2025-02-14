#include "Validator.h"

bool Validator::isPasswordValid(string password)
{
	size_t passLen = password.length();
	int digitsCount = 0, upperCaseCount = 0, lowerCaseCount = 0;

	if (passLen < 4)
	{
		return false;
	}

	for (unsigned int i = 0; i < passLen; i++)
	{
		if (password[i] >= 'a' && password[i] <= 'z')
		{
			lowerCaseCount++;
		}
		else if (password[i] >= 'A' && password[i] <= 'Z')
		{
			upperCaseCount++;
		}
		else if (password[i] >= '0' && password[i] <= '9')
		{
			digitsCount++;
		}
		else if (password[i] == ' ')
		{
			return false;
		}
	}

	return (digitsCount >= 1 && lowerCaseCount >= 1 && upperCaseCount >= 1);
}

bool Validator::isUsernameValid(string username)
{
	return (username.length() != 0 && ((username[0] >= 'a' && username[0] <= 'z') || (username[0] >= 'A' && username[0] <= 'Z')) && username.find(' ') == string::npos);
}