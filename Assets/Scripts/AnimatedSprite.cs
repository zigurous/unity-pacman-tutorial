using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];
    public float animationTime = 0.25f;
    public bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!spriteRenderer.enabled) {
            return;
        }

        animationFrame++;

        if (animationFrame >= sprites.Length && loop) {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart()
    {
        animationFrame = -1;

        Advance();
    }

}
