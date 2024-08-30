using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Text correctAnswerText, incorrectAnswerText;

    [SerializeField]
    private GameObject correctButton, incorrectButton;

    int correctCount, incorrectCount;
    int totalScore;

    [SerializeField]
    private GameObject resultPanel;

    ResultManager resultManager;

    bool isClickable = false;
    void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        incorrectCount = 0;
        correctCount = 0;
        totalScore = 0;

        SelectRandomQuestion();
    }

    void SelectRandomQuestion()
    {
        incorrectButton.GetComponent<RectTransform>().DOLocalMoveX(320f, .2f);
        correctButton.GetComponent<RectTransform>().DOLocalMoveX(-320f, .2f).OnComplete(() => isClickable = true);

        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;

        if (currentQuestion.isCorrect)
        {
            correctAnswerText.text = "DOÐRU CEVAPLADINIZ";
            incorrectAnswerText.text = "YANLIÞ CEVAPLADINIZ";
        }
        else
        {
            correctAnswerText.text = "YANLIÞ CEVAPLADINIZ";
            incorrectAnswerText.text = "DOÐRU CEVAPLADINIZ";
        }
    }

    IEnumerator WaitBetweenQuestionsRoutine()
    {
        unansweredQuestions.Remove(currentQuestion);
        Debug.Log(unansweredQuestions.Count);

        yield return new WaitForSeconds(1f);

        if (unansweredQuestions.Count <= 0)
        {
            resultPanel.SetActive(true);

            resultManager = Object.FindObjectOfType<ResultManager>();
            resultManager.DisplayResults(correctCount, incorrectCount, totalScore);
        }
        else
        {
            SelectRandomQuestion();
        }
    }

    public void OnCorrectButtonClicked()
    {
        if (isClickable)
        {
            isClickable = false;
            if (currentQuestion.isCorrect)
            {
                correctCount++;
                totalScore += 100;
            }
            else
            {
                incorrectCount++;
            }

            incorrectButton.GetComponent<RectTransform>().DOLocalMoveX(1000f, .2f);
            StartCoroutine(WaitBetweenQuestionsRoutine());
        }

    }

    public void OnIncorrectButtonClicked()
    {
        if (isClickable)
        {
            isClickable = false;

            if (!currentQuestion.isCorrect)
            {
                correctCount++;
                totalScore += 100;
            }
            else
            {
                incorrectCount++;
            }
            correctButton.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .2f);
            StartCoroutine(WaitBetweenQuestionsRoutine());
        }
    }
}