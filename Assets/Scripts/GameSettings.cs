using UnityEngine;

public enum Difficulty { Easy, Medium, Hard }

public class GameSettings : MonoBehaviour
{

    public static GameSettings Instance { get; private set; }

    [Header("Selected at runtime")]
    public Difficulty difficulty = Difficulty.Easy;


    [System.Serializable]
    public struct Preset {
        public float pipeSpeed;
        public float spawnRate;
        public float heightOffset;
        public float xRotationOffset;
        public float birdFlap;
    }

    [Header("Presets")]
    public Preset easy = new Preset { pipeSpeed = 7f, spawnRate = 3f, heightOffset = 2f, xRotationOffset = 0f, birdFlap = 16f};
    public Preset medium = new Preset { pipeSpeed = 10f, spawnRate = 3.5f, heightOffset = 5f, xRotationOffset = 10f, birdFlap = 16f};
    public Preset hard = new Preset { pipeSpeed = 12f, spawnRate = 4f, heightOffset = 8f, xRotationOffset = 25f, birdFlap = 16f};

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
