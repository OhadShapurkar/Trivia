#include "DataBase.h"

vector<Question*> resultQuestions;
string lastResult;
string results;
int numResults;
float floatResults;
bool isCalled;
int numOfGames;
int resultCount;
map<string, int> scores;

DataBase::DataBase()
{
	if (sqlite3_open("trivia.db", &db))
	{
		throw;
	}
}

DataBase::~DataBase()
{
	sqlite3_close(db);
}

bool DataBase::isUserExists(string username)
{
	char* zErrMsg = 0;
	string query = "select username from t_users where username='" + username + "';";

	int rc = sqlite3_exec(db, query.c_str(), callbackCount, NULL, &zErrMsg);

	return resultCount == 0 ? false : true;
}

bool DataBase::addNewUser(string username, string password, string email)
{
	int rc;
	char* zErrMsg = 0;
	string query = "insert into t_users values('" + username + "', '" + password + "', '" + email + "');";

	rc = sqlite3_exec(db, query.c_str(), NULL, NULL, &zErrMsg);
	if (rc)
	{
		return false;
	}

	return true;
}

bool DataBase::isUserAndPassMatch(string username, string password)
{
	char* zErrMsg = 0;
	resultCount = 0;
	string query = "select username from t_users where username='" + username + "' and password='" + password + "';";

	sqlite3_exec(db, query.c_str(), callbackCount, NULL, &zErrMsg);

	return resultCount == 0 ? false : true;
}

vector<Question*> DataBase::initQuestions(int questionsNo)
{
	resultQuestions.clear();
	char* zErrMsg = 0;
	resultCount = 0;
	string query = "select * from t_questions order by random() limit " + to_string(questionsNo) + ";";
	int rc = sqlite3_exec(db, query.c_str(), callbackQuestions, NULL, &zErrMsg);
	return resultQuestions;
}
size_t DataBase::insertNewGame()
{
	char* zErrMsg = 0;
	string query = "insert into t_games(status, start_time) values(0, DateTime('now'));";
	sqlite3_exec(db, query.c_str(), NULL, NULL, &zErrMsg);
	return sqlite3_last_insert_rowid(db);
}

bool DataBase::updateGameStatus(int gameId)
{
	int rc;
	char* zErrMsg = 0;
	char gameIdBuffer[32];
	_itoa_s(gameId, gameIdBuffer, 10);
	string query = "update t_games set status=1, end_time=DateTime('now') where game_id=" + string(gameIdBuffer) + ";";

	rc = sqlite3_exec(db, query.c_str(), NULL, NULL, &zErrMsg);
	if (rc)
	{
		return false;
	}

	return true;
}

bool DataBase::addAnswerToPlayer(int gameId, string username, int questionId, string answer, bool isCorrect, int answerTime)
{
	int rc;
	char* zErrMsg = 0;
	char gameIdBuffer[32];
	char questionIdBuffer[32];
	char answerTimeBuffer[32];
	_itoa_s(gameId, gameIdBuffer, 10);
	_itoa_s(questionId, questionIdBuffer, 10);
	_itoa_s(answerTime, answerTimeBuffer, 10);
	string query = "insert into t_players_answers values(" + string(gameIdBuffer) + ", '" + username + "', " + string(questionIdBuffer) + ", '" + answer + "', " + (isCorrect ? "1" : "0") + ", " + string(answerTimeBuffer) + ");";

	rc = sqlite3_exec(db, query.c_str(), NULL, NULL, &zErrMsg);
	if (rc)
	{
		return false;
	}
	return true;
}
string DataBase::getPersonalStatus(string username)
{
	char* zErrMsg = 0;
	string results;
	numOfGames = 0;
	numResults = 0;

	string queryGameCount = "select game_id from t_players_answers where username='" + username + "';";
	string queryRightAnswers = "select count(is_correct) from t_players_answers where username='" + username + "' and is_correct=1;";
	string queryWrongAnswers = "select count(is_correct) from t_players_answers where username='" + username + "' and is_correct=0;";
	string queryAvgTimeForAnswer = "select avg(answer_time) from t_players_answers where username='" + username + "';";

	sqlite3_exec(db, queryGameCount.c_str(), callbackPersonalStatus, NULL, &zErrMsg);
	results += Helper::getPaddedNumber(numOfGames, 4);
	sqlite3_exec(db, queryRightAnswers.c_str(), callbackPersonalStatus, NULL, &zErrMsg);
	results += Helper::getPaddedNumber(numResults, 6);
	sqlite3_exec(db, queryWrongAnswers.c_str(), callbackPersonalStatus, NULL, &zErrMsg);
	results += Helper::getPaddedNumber(numResults, 6);
	sqlite3_exec(db, queryAvgTimeForAnswer.c_str(), callbackPersonalStatus, NULL, &zErrMsg);
	if (floatResults < 10)
	{
		results += '0' + (to_string(floatResults*100)).substr(0,3);
	}
	else
	{
		results += (to_string(floatResults)).substr(0, 4);
	}
	
	return results;
}
string DataBase::getBestScores()
{
	char* zErrMsg;
	int rc;
	string bestScores = "124";
	const char* sql;
	sql = "select username from t_players_answers where is_correct = 1;";

	rc = sqlite3_exec(db, sql, callbackBestScores, 0, &zErrMsg);
	for (int i = 0; i < 3; i++)
	{
		map<string, int>::iterator maxUser = max_element(scores.begin(), scores.end(), [](const pair<string, int>& p1, const pair<string, int>& p2) {return p1.second < p2.second; });
		if (maxUser != scores.end())
		{
			bestScores += Helper::getPaddedNumber((int)(maxUser->first.length()), 2) + maxUser->first + Helper::getPaddedNumber(maxUser->second, 6);
			scores.erase(maxUser);
		}
		else
		{
			bestScores += "00";
		}
	}
	return bestScores;
}
int DataBase::callbackCount(void* unused, int argc, char** argv, char** columns)
{
	resultCount = argc;
	string str = argv[0];
	return 0;
}
int DataBase::callbackQuestions(void* unused, int argc, char** argv, char** columns)
{
	resultQuestions.push_back(new Question(atoi(argv[0]), argv[1], argv[2], argv[3], argv[4], argv[5]));
	return 0;
}
int DataBase::callbackBestScores(void* unused, int argc, char** argv, char** columns)
{
	scores[argv[0]] += 1;
	return 0;
}
int DataBase::callbackPersonalStatus(void* unused, int argc, char** argv, char** columns)
{
	if (atoi(argv[0]) != numResults)
	{
		numOfGames++;
	}
	numResults = atoi(argv[0]); // result
	floatResults = atof(argv[0]);
	return 0;
}

int DataBase::callbackLastResult(void* unused, int argc, char** argv, char** columns)
{
	if (argc > 0)
	{
		lastResult = string(argv[0]);
	}
	return 0;
}