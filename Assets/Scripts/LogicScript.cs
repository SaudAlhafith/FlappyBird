using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    
    public int score;
    public bool gameActive = true;
    public int winningScore = 20;

    public Text scoreText;

    public BirdScript bird;
    public ObstaclesSpawnScript obstacleSpawner;
    
    public GameObject winScreen;
    public GameObject gameOverScreen;

    public AudioClip gameMusic;
    public AudioClip pointSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;
            
    void Awake() {
        if (GameSettings.Instance != null) {
            winningScore = GameSettings.Instance.Current.winningScore;
        }
    }

    void Start() {
        gameActive = true;
        if (AudioManager.Instance != null) {

        AudioManager.Instance.PlayMusic(
            gameMusic ? gameMusic : AudioManager.Instance.defaultGameMusic, true
        );
        }
    }

    public void addScore(int scoreToAdd) {
        score = score + scoreToAdd;
        scoreText.text = score.ToString();
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(pointSound);

        if (score == winningScore) {
            gameWon();
        }
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void continueGame() {
        if (gameActive != false) return;
        winScreen.SetActive(false);
        gameActive = true;
        bird.Freeze(false);
        
        // Pause obstacle spawning and delete existing obstacles to give player time to react
        if (obstacleSpawner != null) {
            obstacleSpawner.PauseSpawning(true);
        }
    }

    public void backToMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void gameWon() {
        if (gameActive != true) return;
        winScreen.SetActive(true);
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(winSound);
        bird.Freeze(true);
        gameActive = false;
    }

    public void gameOver() {
        if (gameActive != true) return;
        gameOverScreen.SetActive(true);
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(gameOverSound);
        gameActive = false;
    }

}
