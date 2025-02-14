#pragma once
#include <string>
#include <vector>
#include <cstdlib>
#include <map>
#include <algorithm>
#include <iterator>
#include "Helper.h"
#include "Question.h"
#include "sqlite3.h"

using namespace std;

class DataBase
{
public:
	DataBase();
	~DataBase();
	bool isUserExists(string username);
	bool addNewUser(string username, string password, string email);
	bool isUserAndPassMatch(string username, string password);
	vector<Question*> initQuestions(int questionsNo);
	string getPersonalStatus(string username);
	string getBestScores();
	size_t insertNewGame();
	bool updateGameStatus(int game_id);
	bool addAnswerToPlayer(int gameId, string username, int questionId, string answer, bool isCorrect, int answerTime);
private:
	static int callbackCount(void* unused, int argc, char** argv, char** columns);
	static int callbackQuestions(void* unused, int argc, char** argv, char** columns);
	static int callbackBestScores(void* unused, int argc, char** argv, char** columns);
	static int callbackPersonalStatus(void* unused, int argc, char** argv, char** columns);
	static int callbackLastResult(void* unused, int argc, char** argv, char** columns);
	sqlite3* db;
	//int resultCount;
};