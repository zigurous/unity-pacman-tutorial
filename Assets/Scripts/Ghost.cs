using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public enum MovementStyle
    {
        Scatter,
        Chase,
        Frightened,
    }

    public AnimatedSprite animatedSprite { get; private set; }
    public Movement movement { get; private set; }
    public MovementStyle movementStyle;
    public Transform target;
    public float scatterDuration = 7.0f;
    public float chaseDuration = 20.0f;
    public bool vulnerable;
    public bool home;

    [Header("Renderers")]
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    private void Awake()
    {
        this.animatedSprite = GetComponent<AnimatedSprite>();
        this.movement = GetComponent<Movement>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);

        Scatter();
    }

    public void RetreatToHome()
    {
        this.home = true;
        this.transform.position = Vector3.zero;

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    public void Frighten(float duration)
    {
        this.vulnerable = true;
        this.movement.speedMultiplier = 0.5f;
        this.movementStyle = MovementStyle.Frightened;

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        CancelInvoke();
        Invoke(nameof(Flash), duration / 2.0f);
        Invoke(nameof(Scatter), duration);
    }

    public void Scatter()
    {
        this.vulnerable = false;
        this.movement.speedMultiplier = 1.0f;
        this.movementStyle = MovementStyle.Scatter;

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;

        CancelInvoke();
        Invoke(nameof(Chase), this.scatterDuration);
    }

    public void Chase()
    {
        this.vulnerable = false;
        this.movement.speedMultiplier = 1.0f;
        this.movementStyle = MovementStyle.Chase;

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;

        CancelInvoke();
        Invoke(nameof(Scatter), this.chaseDuration);
    }

    private void Flash()
    {
        this.blue.enabled = false;
        this.white.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            FindObjectOfType<GameManager>().GhostTouched(this);
        } else if (this.home) {
            this.movement.SetDirection(-this.movement.direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && !this.home)
        {
            switch (this.movementStyle)
            {
                case MovementStyle.Scatter:
                    ScatterMovement(node);
                    break;

                case MovementStyle.Chase:
                    ChaseMovement(node);
                    break;

                case MovementStyle.Frightened:
                    FrightenedMovement(node);
                    break;
            }
        }
    }

    private void ChaseMovement(Node node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (this.target.position - newPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        this.movement.SetDirection(direction);
    }

    private void ScatterMovement(Node node)
    {
        int index = Random.Range(0, node.availableDirections.Count);

        // Prefer not to go back the same direction so increment the index to
        // the next available direction
        if (node.availableDirections[index] == -this.movement.direction && node.availableDirections.Count > 1)
        {
            index++;

            // Wrap the index back around if overflowed
            if (index >= node.availableDirections.Count) {
                index = 0;
            }
        }

        this.movement.SetDirection(node.availableDirections[index]);
    }

    private void FrightenedMovement(Node node)
    {
        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (this.target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance)
            {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        this.movement.SetDirection(direction);
    }

}
