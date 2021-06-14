using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Sprite[] spritesUp;
    public Sprite[] spritesDown;
    public Sprite[] spritesLeft;
    public Sprite[] spritesRight;
    public Sprite[] spritesBlueMode;
    public Sprite[] spritesFlashing;

    public Movement movement { get; private set; }
    public AnimatedSprite animatedSprite { get; private set; }

    public bool vulnerable { get; private set; }

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.animatedSprite = GetComponent<AnimatedSprite>();
    }

    private void OnEnable()
    {
        StopBlueMode();
    }

    public void StartBlueMode(float duration)
    {
        this.vulnerable = true;

        this.animatedSprite.animationSprites = this.spritesBlueMode;

        CancelInvoke(nameof(StartFlashing));
        CancelInvoke(nameof(StopBlueMode));

        Invoke(nameof(StartFlashing), duration * 0.5f);
        Invoke(nameof(StopBlueMode), duration);
    }

    public void StopBlueMode()
    {
        CancelInvoke(nameof(StartFlashing));
        CancelInvoke(nameof(StopBlueMode));

        this.vulnerable = false;
        this.animatedSprite.animationSprites = this.spritesRight;
    }

    private void StartFlashing()
    {
        this.animatedSprite.animationSprites = this.spritesFlashing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            FindObjectOfType<GameManager>().GhostTouched(this);
        }
    }

}
