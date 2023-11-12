using UnityEngine;

[RequireComponent(typeof(Ghost))]
public class Frightened : MonoBehaviour
{
    private Ghost _ghost;

    private void Awake()
    {
        _ghost = GetComponent<Ghost>();
    }

    public void Enable(float duration)
    {
        this.enabled = true;

        _ghost.body.enabled = false;
        _ghost.eyes.enabled = false;
        _ghost.blue.enabled = true;
        _ghost.flashing.enabled = false;

        CancelInvoke();
        Invoke(nameof(StartFlashing), duration * 0.5f);
        Invoke(nameof(Disable), duration);
    }

    public void Disable()
    {
        this.enabled = false;

        _ghost.body.enabled = true;
        _ghost.eyes.enabled = true;
        _ghost.blue.enabled = false;
        _ghost.flashing.enabled = false;

        CancelInvoke();
    }

    private void StartFlashing()
    {
        _ghost.blue.enabled = false;
        _ghost.flashing.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled) {
            return;
        }

        Node node = other.GetComponent<Node>();

        if (node != null)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back the same direction so increment the index
            // to the next available direction
            if (node.availableDirections[index] == -_ghost.movement.direction)
            {
                if (node.availableDirections.Count > 1)
                {
                    index++;

                    // Wrap the index back around if overflowed
                    if (index >= node.availableDirections.Count) {
                        index = 0;
                    }
                }
            }

            _ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
