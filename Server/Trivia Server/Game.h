#pragma once

#include <vector>
#include <map>
#include <string>
#include "Question.h"
#include "Helper.h"
#include "DataBase.h"

using namespace std;

class User;

class Game
{
public:
	Game(const vector<User*>& players, int questionsNo, DataBase& db);//create game by a vector of players that were in one room, number of questions and database
	~Game();//delete the game - empty the vector of questions
	void sendFirstQuestion();//send to all users in players vector first question
	void handleFinishGame();//finish the game send to all users there score and close the game
	bool handleNextTurn();//make sure that there are users in this game if not finish game else check if this is the last question and finish game else, give the players new questions
	bool handleAnswerFromUser(User* user, int answerNo, int time);//check the answer of players if right returns true else returns false
	bool leaveGame(User*);//remove this player from this game
	int getID();//return the id of this game
private:
	bool insertGameToDB();
	void initQuestionsFromDB();
	void sendQuestionToAllUsers();
	vector<Question*> _questions;
	vector<User*> _players;
	int _questions_no;
	int _currQuestionIndex;
	DataBase& _db;
	map<string, int> _results;
	int _currentTurnAnswers;
	int _id;
};