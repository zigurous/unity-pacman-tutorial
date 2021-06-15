using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer flashing;

    public AnimatedSprite animatedSprite { get; private set; }
    public Movement movement { get; private set; }
    public Chase chase { get; private set; }
    public Scatter scatter { get; private set; }
    public Frightened frightened { get; private set; }

    private void Awake()
    {
        this.animatedSprite = GetComponent<AnimatedSprite>();
        this.movement = GetComponent<Movement>();
        this.chase = GetComponent<Chase>();
        this.scatter = GetComponent<Scatter>();
        this.frightened = GetComponent<Frightened>();
        this.frightened.enabled = false;
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            FindObjectOfType<GameManager>().GhostTouched(this);
        }
    }

}
