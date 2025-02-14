#pragma once

#include <iostream>
#include <algorithm>
#include "Helper.h"
using namespace std;

class Question
{

public:
	Question(int id, string question, string correctAnswer, string answer2, string answer3, string answer4);//create question by id, question, correct answers, and there another answers
	string getQuestion();//returns question
	string* getAnswers();//returns array of answers
	int getCorrectAnswerIndex();//return correct answer index
	int getid();//return this question id;

private:
	string _question;
	string _answers[4];
	int _correctAnswerIndex;
	int _id;
};