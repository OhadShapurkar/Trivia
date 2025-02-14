#include "Question.h"


Question::Question(int id, string question, string correctAnswer, string answer2, string answer3, string answer4)
{
	int value[4] = { 0, 1, 2, 3 };

	random_shuffle(std::begin(value), std::end(value));

	this->_question = question;
	this->_id = id;
	this->_correctAnswerIndex = value[0];
	this->_answers[value[0]] = correctAnswer;
	this->_answers[value[1]] = answer2;
	this->_answers[value[2]] = answer3;
	this->_answers[value[3]] = answer4;
	cout << "Correct answer index = " << _correctAnswerIndex;
}

string Question::getQuestion()
{
	return this->_question;
}

string* Question::getAnswers()
{
	return this->_answers;
}

int Question::getCorrectAnswerIndex()
{
	return this->_correctAnswerIndex;
}

int Question::getid()
{
	return this->_id;
}