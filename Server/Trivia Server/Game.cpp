#include "Game.h"
#include "User.h"

Game::Game(const vector<User*>& players, int questionsNo, DataBase& db) : _players(players), _questions_no(questionsNo), _db(db)
{
	_currentTurnAnswers = 0;
	_currQuestionIndex = 0;
	_id = 0;
	if (!insertGameToDB())
	{
		throw;
	}
	initQuestionsFromDB();
	for (int i = 0; i < _players.size(); i++)
	{
		_results[players[i]->getUsername()] = 0;
		_players[i]->setGame(this);
	}
}

Game::~Game()
{
	cout << "GAME REMOVED" << endl;
	this->_questions.clear();
}

bool Game::insertGameToDB()
{
	if ((_id = _db.insertNewGame()))
	{
		return true;
	}
	return false;
}

void Game::initQuestionsFromDB()
{
	_questions = _db.initQuestions(_questions_no);
	cout << "QUEsTIONS!!!!!!" << endl;
	cout << _questions[0]->getCorrectAnswerIndex() << endl;
	cout << _questions[1]->getCorrectAnswerIndex() << endl;
	cout << _questions[2]->getCorrectAnswerIndex() << endl;
	cout << _questions[3]->getCorrectAnswerIndex() << endl;
	cout << _questions[4]->getCorrectAnswerIndex() << endl;
}

void Game::sendQuestionToAllUsers()
{
	cout << "In sendQuestionToAllUsers" << endl;
	string* answers = _questions[_currQuestionIndex]->getAnswers();
	char answersLengthBuffer[4][32];
	char questionLengthBuffer[32];
	_itoa_s(answers[0].length(), answersLengthBuffer[0], 10);
	_itoa_s(answers[1].length(), answersLengthBuffer[1], 10);
	_itoa_s(answers[2].length(), answersLengthBuffer[2], 10);
	_itoa_s(answers[3].length(), answersLengthBuffer[3], 10);
	_itoa_s(_questions[_currQuestionIndex]->getQuestion().length(), questionLengthBuffer, 10);
	string message = "118" + Helper::getPaddedNumber(_questions[_currQuestionIndex]->getQuestion().length(), 3) + _questions[_currQuestionIndex]->getQuestion() + Helper::getPaddedNumber(answers[0].length(), 3) + answers[0] + Helper::getPaddedNumber(answers[1].length(), 3) + answers[1] + Helper::getPaddedNumber(answers[2].length(), 3) + answers[2] + Helper::getPaddedNumber(answers[3].length(), 3) + answers[3];
	_currentTurnAnswers = 0;
	for (int i = 0; i < _players.size(); i++)
	{
		try
		{
			_players[i]->send(message);
		}
		catch (exception e)
		{
		}
	}
}

void Game::sendFirstQuestion()
{
	sendQuestionToAllUsers();
}

void Game::handleFinishGame()
{
	cout << "In handleFinishGame" << endl;
	string msg = "121" + to_string(_players.size());
	for (vector<User*>::iterator it = _players.begin(); it != _players.end(); ++it)
	{
		msg += Helper::getPaddedNumber((*it)->getUsername().length(), 2) + (*it)->getUsername() + Helper::getPaddedNumber(_results[(*it)->getUsername()], 2);
	}
	cout << "message: " << msg << endl;
	for (vector<User*>::iterator it = _players.begin(); it != _players.end(); ++it)
	{
		(*it)->send(msg);
	}
	_db.updateGameStatus(_id);
}

bool Game::handleNextTurn()
{
	cout << "In handle next turn" << endl;
	if (_players.size() == 0)
	{
		handleFinishGame();
		return false;
	}
	if (_currentTurnAnswers == _players.size())
	{
		if (_currQuestionIndex == (_questions.size()-1))
		{
			_currQuestionIndex++;
			handleFinishGame();
			return false;
		}
		else
		{
			_currQuestionIndex++;
			_currentTurnAnswers = 0;
			sendQuestionToAllUsers();
			return true;
		}
	}
	return true;
}

bool Game::handleAnswerFromUser(User* user, int answerNo, int time)
{
	cout << "In handleAnswerFromUser" << endl;
	cout << "currQuestionIndex = " << _currQuestionIndex << endl;
	cout << "questionsSize = " << _questions.size() << endl;
	cout << "answerno " << answerNo << endl;
	string msg = "120";
	bool isCorrect = false;
	if (_questions[_currQuestionIndex]->getCorrectAnswerIndex() == answerNo-1)
	{
		_results[user->getUsername()]++;
		isCorrect = true;
	}
	_db.addAnswerToPlayer(_id, user->getUsername(), _questions[_currQuestionIndex]->getid(), (answerNo == 5) ? "" : _questions[_currQuestionIndex]->getAnswers()[answerNo-1], isCorrect, time);
	msg += isCorrect ? "1" : "0";
	user->send(msg);
	_currentTurnAnswers++;
	handleNextTurn();
	return (!(this->_currQuestionIndex == this->_questions_no));
}

bool Game::leaveGame(User* currUser)
{
	if (_players.size() == 0)
	{
		return false;
	}
	for (int i = 0; i < _players.size(); i++)
	{
		if (_players[i] == currUser)
		{
			_players.erase(_players.begin() + i);
			return true;
		}
	}
}

int Game::getID()
{
	return _id;
}