using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{

    [Header("UI")]
    public TMP_Dropdown difficultyDropdown;
    public AudioClip startMenuMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(startMenuMusic ? startMenuMusic
                                                           : AudioManager.Instance.defaultMenuMusic, true);
        }
    }

    public void OnPlayPressed()
    {
        if (GameSettings.Instance != null)
        {
            // let's check what the enum wants the value to be like
            GameSettings.Instance.difficulty = (Difficulty)difficultyDropdown.value;
        }
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        // Makes the Exit button work in the Editor for testing
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
