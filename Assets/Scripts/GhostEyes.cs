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
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (this.movement.direction == Vector2.up) {
            this.spriteRenderer.sprite = this.up;
        }
        else if (this.movement.direction == Vector2.down) {
            this.spriteRenderer.sprite = this.down;
        }
        else if (this.movement.direction == Vector2.left) {
            this.spriteRenderer.sprite = this.left;
        }
        else if (this.movement.direction == Vector2.right) {
            this.spriteRenderer.sprite = this.right;
        }
    }

}
