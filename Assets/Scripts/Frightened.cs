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

}
