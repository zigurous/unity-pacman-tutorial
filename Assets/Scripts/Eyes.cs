using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Eyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private SpriteRenderer _spriteRenderer;
    private Movement _movement;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (_movement.direction == Vector2.up) {
            _spriteRenderer.sprite = this.up;
        }
        else if (_movement.direction == Vector2.down) {
            _spriteRenderer.sprite = this.down;
        }
        else if (_movement.direction == Vector2.left) {
            _spriteRenderer.sprite = this.left;
        }
        else if (_movement.direction == Vector2.right) {
            _spriteRenderer.sprite = this.right;
        }
    }

}
