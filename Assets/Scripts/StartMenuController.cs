using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuController : MonoBehaviour
{

    [Header("UI")]
    public TMP_Dropdown difficultyDropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnPlayPressed()
    {
        if (GameSettings.Instance != null)
        {
            // let's check what the enum wants the value to be like
            GameSettings.Instance.difficulty = (Difficulty)difficultyDropdown.value;
        }
        SceneManager.LoadScene("Game");
    }
}
