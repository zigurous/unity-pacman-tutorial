using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = this.connection.position;
        position.z = other.transform.position.z;
        other.transform.position = position;
    }

}
