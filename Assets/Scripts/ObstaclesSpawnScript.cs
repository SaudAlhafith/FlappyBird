using UnityEngine;

public class ObstaclesSpawnScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject obstacle;

    public float spawnRate = 2f;
    public float heightOffset = 7f; // Maximum 7
    public float xRotationOffset = 0f; // Maximum 25
    private float timer = 0;

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
        Instantiate(obstacle, spawnPosition, Quaternion.Euler(0, 0, xRotation));
    }
}
