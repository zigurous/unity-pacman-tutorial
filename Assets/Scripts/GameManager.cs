using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform pellets;
    private Ghost[] _ghosts;
    private Pacman _pacman;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    private int _score;
    private int _lives;

    private void Awake()
    {
        _pacman = FindObjectOfType<Pacman>();
        _ghosts = FindObjectsOfType<Ghost>();
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (_lives <= 0 && Input.anyKey) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        this.gameOverText.enabled = false;

        foreach (Transform pellet in this.pellets) {
            pellet.gameObject.SetActive(true);
        }

        for (int i = 0; i < _ghosts.Length; i++) {
            _ghosts[i].ResetState();
        }

        _pacman.gameObject.SetActive(true);
    }

    private void RestartRound()
    {
        for (int i = 0; i < _ghosts.Length; i++) {
            _ghosts[i].ResetState();
        }

        _pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        this.gameOverText.enabled = true;

        for (int i = 0; i < _ghosts.Length; i++) {
            _ghosts[i].gameObject.SetActive(false);
        }

        _pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        _lives = lives;

        this.livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        _score = score;

        this.scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        _pacman.gameObject.SetActive(false);

        SetLives(_lives - 1);

        if (_lives > 0) {
            Invoke(nameof(RestartRound), 3.0f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(_score + 200);
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(_score + pellet.points);

        if (!HasRemainingPellets())
        {
            _pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

}
