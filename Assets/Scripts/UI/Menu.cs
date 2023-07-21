using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject endGame;


    private void OnEnable()
    {
        Player.playerWin += EndGame;
    }

    private void OnDisable()
    {
        Player.playerWin -= EndGame;
    }

    private void Awake()
    {
        game.SetActive(true);
        pause.SetActive(false);
        endGame.SetActive(false);
    }
    public void OnPause()
    {
        game.SetActive(false);
        pause.SetActive(true);
        Invoke("StopGame", 0.4f);
    }


    public void OnContinue()
    {
        Time.timeScale = 1.0f;
        game.SetActive(true);
        pause.SetActive(false);

    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    private void EndGame()
    {
        endGame.SetActive(true);
    }
}
