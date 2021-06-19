using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;

    protected override void Eat()
    {
        base.Eat();

        Ghost[] ghosts = FindObjectsOfType<Ghost>();

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(this.duration);
        }
    }

}
