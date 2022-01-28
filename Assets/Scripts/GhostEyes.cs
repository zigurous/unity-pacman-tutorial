using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Movement movement { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (movement.direction == Vector2.up) {
            spriteRenderer.sprite = up;
        }
        else if (movement.direction == Vector2.down) {
            spriteRenderer.sprite = down;
        }
        else if (movement.direction == Vector2.left) {
            spriteRenderer.sprite = left;
        }
        else if (movement.direction == Vector2.right) {
            spriteRenderer.sprite = right;
        }
    }

}
