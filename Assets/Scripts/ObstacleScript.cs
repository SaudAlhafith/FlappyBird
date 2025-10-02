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
            AdjustGapSize(GameSettings.Instance.Current.gapSize);
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    
    void AdjustGapSize(float gapMultiplier)
    {
        Transform topPipe = transform.Find("Top");
        Transform bottomPipe = transform.Find("Bottom");
        Transform middleArea = transform.Find("Middle");
        
        if (topPipe == null || bottomPipe == null)
        {
            Debug.LogWarning("Could not find Top or Bottom pipes in obstacle!");
            return;
        }
        
        // Calculate CURRENT gap from existing positions
        float currentTopY = topPipe.localPosition.y;
        float currentBottomY = bottomPipe.localPosition.y;
        float currentGap = currentTopY - currentBottomY;
        
        // Calculate new gap proportionally
        float newGap = currentGap * gapMultiplier;
        
        // Calculate how much to move each pipe (they move towards center)
        float gapReduction = currentGap - newGap;
        float moveAmount = gapReduction / 2f;
        
        // Move top pipe DOWN and bottom pipe UP by moveAmount
        topPipe.localPosition = new Vector3(topPipe.localPosition.x, currentTopY - moveAmount, topPipe.localPosition.z);
        bottomPipe.localPosition = new Vector3(bottomPipe.localPosition.x, currentBottomY + moveAmount, bottomPipe.localPosition.z);
        
        // Adjust middle area BoxCollider2D size if it exists
        if (middleArea != null)
        {
            BoxCollider2D middleCollider = middleArea.GetComponent<BoxCollider2D>();
            if (middleCollider != null)
            {
                // Update the collider size to match the new gap
                Vector2 newSize = middleCollider.size;
                newSize.y = newGap;
                middleCollider.size = newSize;
            }
        }
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
