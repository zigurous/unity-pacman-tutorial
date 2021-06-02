using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points;

    protected virtual void Eat()
    {
        this.gameObject.SetActive(false);

        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            Eat();
        }
    }

}
