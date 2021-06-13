using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Sprite[] spritesUp;
    public Sprite[] spritesDown;
    public Sprite[] spritesLeft;
    public Sprite[] spritesRight;
    public Sprite[] spritesBlueMode;
    public Sprite[] spritesFlashing;

    private AnimatedSprite _animatedSprite;
    private Vector3 _startingPosition;

    public bool vulnerable { get; private set; }

    private void Awake()
    {
        _animatedSprite = GetComponent<AnimatedSprite>();
        _startingPosition = this.transform.position;
    }

    public void ResetState()
    {
        this.transform.position = _startingPosition;
        this.gameObject.SetActive(true);

        StopBlueMode();
    }

    public void StartBlueMode(float duration)
    {
        this.vulnerable = true;

        _animatedSprite.animationSprites = this.spritesBlueMode;

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

        _animatedSprite.animationSprites = this.spritesRight;
    }

    private void StartFlashing()
    {
        _animatedSprite.animationSprites = this.spritesFlashing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            FindObjectOfType<GameManager>().GhostTouched(this);
        }
    }

}