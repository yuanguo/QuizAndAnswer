﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using AgileReaction.TriviaFramework;
using AgileReaction.TriviaFramework.Core;

public class GamePlay : MonoBehaviour 
{
	static string QuestionStringFormat = "<color=#fee300ff>Q : </color>{0}";
	static string AnswerStringFormat1 = "<color=#fee300ff>(a) </color>{0}";
	static string AnswerStringFormat2 = "<color=#fee300ff>(b) </color>{0}";
	static string AnswerStringFormat3 = "<color=#fee300ff>(c) </color>{0}";
	static string CorrectAnswersCounterStrFormat = "{0} / {1}";

	static int AllQuestions = 30;

	public GameLogic m_gameLogic = null;

	public Text m_txtAnswerCounter = null;

	public Text m_txtQuestion = null;
	public List<Text> m_txtAnswers = new List<Text>();

	public List<Apart> m_aprts = new List<Apart>();

	public AudioClip m_audioClipRight = null;
	public AudioClip m_audioClipWrong = null;

	public AudioSource m_audioSourceGame = null;

	public AudioClip m_bgSoundClip = null;
	public AudioSource m_bgSoundSource = null;


	TriviaDatabaseAccess triviaData;
	private List<TriviaAnswer> answers;

	private bool isAnswered = false;

	public int m_correctAnswers = 0;

	public float m_updateDeltaTime = 1.0f;


	private void Start()
	{
	}

	void OnEnable()
	{
		initTriviaData();

		initUIs();

		m_correctAnswers = 1;
		_updateApartment(m_correctAnswers);

		_updateAsnwersCounter(m_correctAnswers);

		m_bgSoundSource.clip = m_bgSoundClip;
		m_bgSoundSource.loop = true;
		m_bgSoundSource.Play();
	}


	private void initUIs()
	{
		for (int nIdx = 0; nIdx < m_aprts.Count; nIdx++)
			m_aprts[nIdx].CompletedFloor(0);
	}

	private void initTriviaData()
	{
		triviaData = TriviaDatabaseAccess.instance;
		if (triviaData)
		{
			triviaData.LoadTrivia(true);
			NewQuestion();
		}
	}

	private void NewQuestion()
	{
		isAnswered = false;
		m_txtQuestion.text = string.Format(QuestionStringFormat, triviaData.GetQuestion());

		answers = triviaData.GetRandomizeAnswers(); // gets a mix of answers

		//Set each button text to a answer
		m_txtAnswers[0].text = string.Format(AnswerStringFormat1, answers[0].answer);
		m_txtAnswers[1].text = string.Format(AnswerStringFormat2, answers[1].answer);
		m_txtAnswers[2].text = string.Format(AnswerStringFormat3, answers[2].answer);
	}


	private void SubmitAnswer(int btnPressed)
	{
		if (isAnswered)
		{
			return; //prevents from answering again till new question
		}

		isAnswered = true;

		if (answers[btnPressed].isTrue) // Test if button pressed number is the correct button number
		{
			//You got the Question Right
			m_correctAnswers++;
			_updateApartment(m_correctAnswers);
			_updateAsnwersCounter(m_correctAnswers);

			m_audioSourceGame.clip = m_audioClipRight;
			m_audioSourceGame.loop = false;
			m_audioSourceGame.Play();
		}
		else
		{
			//You got the Question Wrong
			m_gameLogic.PlayGameOver();
			m_audioSourceGame.clip = m_audioClipWrong;
			m_audioSourceGame.loop = false;
			m_audioSourceGame.Play();
			return;
		}

		if (triviaData.trivia.Count > 1) // checks to see if that was the last one
		{
			triviaData.PopTrivia(); // Removes Trivia from the list so its not repeated and
			Invoke("NewQuestion", m_updateDeltaTime); //Changes question in 5 seconds
		}
		else
		{
			Debug.Log("No more trivia");
		}
	}

	private void _updateApartment (int _correctAnswerNum)
	{

		if (_correctAnswerNum > 0)
		{
			int nFloor = m_correctAnswers - m_aprts[0].allFloors - m_aprts[1].allFloors - m_aprts[2].allFloors;
			if (nFloor > 0)
			{
				m_aprts[3].CompletedFloor(nFloor);
				m_aprts[2].CompletedFloor(_correctAnswerNum);
			}
			else
			{
				nFloor = m_correctAnswers - m_aprts[0].allFloors - m_aprts[1].allFloors;
				if (nFloor > 0)
				{
					m_aprts[2].CompletedFloor(nFloor);
					m_aprts[1].CompletedFloor(_correctAnswerNum);
				}
				else
				{
					nFloor = m_correctAnswers - m_aprts[0].allFloors;
					if (nFloor > 0)
					{
						m_aprts[1].CompletedFloor(nFloor);
						m_aprts[0].CompletedFloor(_correctAnswerNum);
					}
					else
					{
						nFloor = m_correctAnswers;
						if (nFloor > 0)
							m_aprts[0].CompletedFloor(nFloor);
					}
				}
			}
		}
	}

	void _updateAsnwersCounter(int _correctAnswer)
	{
		m_txtAnswerCounter.text = string.Format(CorrectAnswersCounterStrFormat, (_correctAnswer - 1), AllQuestions);
	}

	#region Public Methods

	public void BtnAnswer0()
	{
		SubmitAnswer(0);
	}

	public void BtnAnswer1()
	{
		SubmitAnswer(1);
	}

	public void BtnAnswer2()
	{
		SubmitAnswer(2);
	}


	public void BtnReply()
	{
		initTriviaData();

		initUIs();

		m_correctAnswers = 1;
		_updateApartment(m_correctAnswers);

		_updateAsnwersCounter(m_correctAnswers);

		m_gameLogic.PlayGameScene();
	}
	#endregion Public Methods
}
