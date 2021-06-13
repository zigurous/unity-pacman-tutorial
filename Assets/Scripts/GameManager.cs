using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform pellets;
    private Ghost[] _ghosts;
    private Pacman _pacman;

    public Text scoreText;
    private int _score;

    public Text livesText;
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

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        _pacman.ResetPosition();

        for (int i = 0; i < _ghosts.Length; i++) {
            _ghosts[i].ResetPosition();
        }

        foreach (Transform pellet in this.pellets) {
            pellet.gameObject.SetActive(true);
        }
    }

    private void GameOver()
    {

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

    public void GhostTouched(Ghost ghost)
    {
        if (ghost.vulnerable)
        {

        }
        else
        {
            SetLives(_lives - 1);

            if (_lives > 0) {
                StartCoroutine(Transition(NewRound));
            } else {
                StartCoroutine(Transition(GameOver));
            }
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(_score + pellet.points);

        if (!HasRemainingPellets()) {
            StartCoroutine(Transition(NewRound));
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

    private IEnumerator Transition(System.Action onTransition)
    {
        Time.timeScale = 0.0f;

        yield return new WaitForSecondsRealtime(1.5f);

        onTransition.Invoke();

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1.0f;
    }

}
