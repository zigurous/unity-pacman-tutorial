using UnityEngine;

public class Pacman : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = Vector2.right;
    private Vector2 _desiredDirection;

    public float speed = 8.0f;
    public LayerMask obstacleLayer;
    public Bounds levelBounds;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            SetDirection(Vector2.up);
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            SetDirection(Vector2.down);
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            SetDirection(Vector2.left);
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            SetDirection(Vector2.right);
        } else if (_desiredDirection != Vector2.zero) {
            SetDirection(_desiredDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 translation = _direction * this.speed * Time.fixedDeltaTime;
        Vector2 position = _rigidbody.position;

        _rigidbody.MovePosition(position + translation);

        CheckLoopAround();
    }

    private void SetDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 0.5f, this.obstacleLayer);

        if (hit.collider == null)
        {
            _direction = direction;
            _desiredDirection = Vector2.zero;

            this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg, Vector3.forward);
        }
        else
        {
            _desiredDirection = direction;
        }
    }

    private void CheckLoopAround()
    {
        if (this.transform.position.x < this.levelBounds.min.x)
        {
            Vector2 position = _rigidbody.position;
            position.x = this.levelBounds.max.x;
            _rigidbody.position = position;
        }
        else if (this.transform.position.x > this.levelBounds.max.x)
        {
            Vector2 position = _rigidbody.position;
            position.x = this.levelBounds.min.x;
            _rigidbody.position = position;
        }
    }

}
