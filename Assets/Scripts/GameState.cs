using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] InputEv input;
    public bool GameActive { get; set;}

    private void Awake()
    {
        GameEventManagerRunner.StartGame += ChangeStateActive;
        GameEventManagerRunner.GameOver += ChangeStateNonActive;
        input.inputSpace += StartGame;
    }

    public void ChangeStateNonActive()
    {
        GameActive = false;
    }

    public void ChangeStateActive()
    {
        GameActive=true;
    }

    public void StartGame()
    {
        if (!GameActive) GameEventManagerRunner.InvokeGameStart();

    }
}
