using UnityEngine;

public class ObstaclesSpawnScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject obstacle;

    public float spawnRate = 2f;
    public float heightOffset = 7f; // Maximum 7
    public float xRotationOffset = 0f; // Maximum 25
    private float timer = 0;
    private bool isPaused = false;
    private float pauseDuration = 3f; // 3 seconds pause after continue
    
    // Track the last few spawned obstacles
    private System.Collections.Generic.List<GameObject> recentObstacles = new System.Collections.Generic.List<GameObject>();
    private int maxTrackedObstacles = 3; // Remember last 3 obstacles

    void Awake()
    {
        if (GameSettings.Instance != null)
        {
            spawnRate = GameSettings.Instance.Current.spawnRate;
            heightOffset = GameSettings.Instance.Current.heightOffset;
            xRotationOffset = GameSettings.Instance.Current.xRotationOffset;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heightOffset = Mathf.Min(heightOffset, 7);
        xRotationOffset = Mathf.Min(xRotationOffset, 25);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (!logic.gameActive)
        {
            return;
        }
        
        // Don't spawn obstacles if paused
        if (isPaused)
        {
            return;
        }
        
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnObstacle();
            timer = 0;
        }
    }

    void spawnObstacle()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        float xRotation = Random.Range(-xRotationOffset, xRotationOffset);

        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);
        GameObject newObstacle = Instantiate(obstacle, spawnPosition, Quaternion.Euler(0, 0, xRotation));
        
        // Track this obstacle
        recentObstacles.Add(newObstacle);
        
        // Keep only the last few obstacles
        if (recentObstacles.Count > maxTrackedObstacles)
        {
            recentObstacles.RemoveAt(0);
        }
    }

    public void PauseSpawning(bool deleteExisting = false)
    {
        isPaused = true;
        
        // Delete existing obstacles if requested
        if (deleteExisting)
        {
            foreach (GameObject obstacle in recentObstacles)
            {
                if (obstacle != null)
                {
                    Destroy(obstacle);
                }
            }
            recentObstacles.Clear();
        }
        
        // Automatically resume after pauseDuration seconds
        Invoke(nameof(ResumeSpawning), pauseDuration);
    }

    private void ResumeSpawning()
    {
        isPaused = false;
        // Reset timer to give player a bit more time
        timer = 0;
    }
}
