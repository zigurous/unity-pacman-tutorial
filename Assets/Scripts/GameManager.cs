using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform pellets;
    public Pacman pacman;

    public Text scoreText;
    private int _score;

    public Text livesText;
    private int _lives;

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
        this.pacman.ResetPosition();

        foreach (Transform pellet in this.pellets) {
            pellet.gameObject.SetActive(true);
        }
    }

    private IEnumerator NewRoundSequence()
    {
        Time.timeScale = 0.0f;
        float nextRoundTime = Time.realtimeSinceStartup + 2.0f;

        while (Time.realtimeSinceStartup < nextRoundTime) {
            yield return null;
        }

        Time.timeScale = 1.0f;
        NewRound();
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

        }
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(_score + pellet.points);

        if (!HasRemainingPellets()) {
            StartCoroutine(NewRoundSequence());
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
