using UnityEngine;

public enum Difficulty { Easy, Medium, Hard }

public class GameSettings : MonoBehaviour
{

    public static GameSettings Instance { get; private set; }

    [Header("Selected at runtime")]
    public Difficulty difficulty = Difficulty.Easy;


    [System.Serializable]
    public struct Preset {
        public int winningScore;
        public float pipeSpeed;
        public float spawnRate;
        public float heightOffset;
        public float xRotationOffset;
        public float birdFlap;
    }

    [Header("Presets")]
    public Preset easy = new Preset { winningScore = 10, pipeSpeed = 10f, spawnRate = 3f, heightOffset = 2f, xRotationOffset = 0f, birdFlap = 16f};
    public Preset medium = new Preset { winningScore = 25, pipeSpeed = 20f, spawnRate = 2f, heightOffset = 5f, xRotationOffset = 10f, birdFlap = 16f};
    public Preset hard = new Preset { winningScore = 50, pipeSpeed = 30f, spawnRate = 1f, heightOffset = 8f, xRotationOffset = 25f, birdFlap = 16f};

    public Preset Current =>
        difficulty == Difficulty.Easy ? easy :
        difficulty == Difficulty.Medium ? medium : hard;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
