using UnityEngine;

public class LoopShiftByTime : MonoBehaviour
{
    [Tooltip("World speed in units/sec (match pipe speed).")]
    public float worldSpeed = 3f;

    [Range(0f, 2f)]
    [Tooltip("Parallax factor (Ground=1, Mid=~0.5).")]
    public float parallax = 1f;

    [Tooltip("Width of ONE repeating tile in world units. Leave 0 to auto from first SpriteRenderer.")]
    public float segmentWidth = 0f;

    [Tooltip("Optional: a reference sprite that represents ONE tile.")]
    public SpriteRenderer referenceTile;

    float startX;

    void Awake()
    {
        startX = transform.position.x;

        if (segmentWidth <= 0f)
        {
            var sr = referenceTile ? referenceTile : GetComponentInChildren<SpriteRenderer>();
            if (sr) segmentWidth = sr.bounds.size.x;
            else Debug.LogError($"{name}: set segmentWidth or assign referenceTile.");
        }
    }

    void Update()
    {
        float dx = worldSpeed * parallax * Time.deltaTime;
        transform.position += Vector3.left * dx;

        // Wrap by exactly one tile width (can wrap multiple times if FPS hitch)
        while (transform.position.x <= startX - segmentWidth)
            transform.position += Vector3.right * segmentWidth;
    }
}
