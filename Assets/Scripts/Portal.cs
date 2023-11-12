using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore the z-axis because it is used for draw depth
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }

}
