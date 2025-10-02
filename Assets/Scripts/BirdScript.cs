using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public LogicScript logic;

    public Rigidbody2D rb;
    public float flapStrength = 16f;

    private float _defaultGravity;
    
    void Awake()
    {
        if (GameSettings.Instance != null)
        {
            flapStrength = GameSettings.Instance.Current.birdFlap;
        }
        _defaultGravity = rb.gravityScale;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && logic.gameActive)
        {
            rb.linearVelocity = Vector2.up * flapStrength;
        }

        if (Mathf.Abs(rb.position.y) > 35)
        {
            logic.gameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        logic.gameOver();
    }

    public void Freeze(bool on) {
        if (on) {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;

            enabled = false;
        } else {
            rb.gravityScale = _defaultGravity;
            enabled = true;
        }
    }
}
