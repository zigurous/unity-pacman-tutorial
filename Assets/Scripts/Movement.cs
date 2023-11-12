using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (this.nextDirection != Vector2.zero) {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * Time.fixedDeltaTime;

        this.rigidbody.MovePosition(position + translation);
    }

    public bool SetDirection(Vector2 direction)
    {
        if (!Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
            return true;
        }
        else
        {
            this.nextDirection = direction;
            return false;
        }
    }

    public void SetNextDirection(Vector2 direction)
    {
        this.nextDirection = direction;
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

}
