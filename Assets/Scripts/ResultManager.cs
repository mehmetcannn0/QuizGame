using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text correctText, incorrectText, scoreText;

    [SerializeField]
    private GameObject firstStar, secondStar, thirdStar;

    public void DisplayResults(int correctCount, int incorrectCount, int score)
    {
        correctText.text = correctCount.ToString();
        incorrectText.text = incorrectCount.ToString();
        scoreText.text = score.ToString();

        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        int totalQuestions = correctCount + incorrectCount;

        if (totalQuestions == 0) return;

        float correctPercentage = (float)correctCount / totalQuestions;

        if (correctPercentage >= 0.8f) // %80 veya daha fazla doðru
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
            thirdStar.SetActive(true);
        }
        else if (correctPercentage >= 0.5f) // %50 - %79 arasý doðru
        {
            firstStar.SetActive(true);
            secondStar.SetActive(true);
        }
        else if (correctPercentage > 0) // %1 - %49 arasý doðru
        {
            firstStar.SetActive(true);
        }
    }
    public void RestartGame()
    {
        Debug.Log("restart fonksýyonu");
        SceneManager.LoadScene("GamePlay");
    }
}
