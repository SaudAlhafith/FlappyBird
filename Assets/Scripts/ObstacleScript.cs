using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public LogicScript logic;

    public float moveSpeed = 5f;
    public float deadZone = -40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameSettings.Instance != null)
        {
            moveSpeed = GameSettings.Instance.Current.pipeSpeed;
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!logic.gameActive)
        {
            return;
        }
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
