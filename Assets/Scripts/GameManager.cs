using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    private Vector3 _startingPosition;

    public Text scoreText;
    private int _score;
    private int _lives;

    private void Start()
    {
        _startingPosition = this.pacman.transform.position;

        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        this.pacman.transform.position = _startingPosition;

        // TODO
    }

    private void SetLives(int lives)
    {
        _lives = lives;

        // TODO: update UI
    }

    private void SetScore(int score)
    {
        _score = score;

        this.scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void IncreaseScore(int amount)
    {
        SetScore(_score + amount);
    }

}
