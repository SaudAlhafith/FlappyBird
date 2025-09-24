using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    
    public int score;
    public bool gameActive = true;

    public Text scoreText;
    public GameObject gameOverScreen;

    void Start() {
        gameActive = true;
    }

    public void addScore(int scoreToAdd) {
        score = score + scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        gameActive = false;
    }

}
