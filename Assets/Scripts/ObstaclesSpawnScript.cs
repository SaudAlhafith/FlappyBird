using UnityEngine;

public class ObstaclesSpawnScript : MonoBehaviour
{
    public GameObject obstacle;
    public float spawnRate = 2;
    public float heightOffset = 10;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);
        Instantiate(obstacle, spawnPosition, transform.rotation);
    }
}
