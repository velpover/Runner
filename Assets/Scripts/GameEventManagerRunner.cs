using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManagerRunner 
{
    public delegate void GameEvent();

    public static event GameEvent StartGame, GameOver;

    public static void InvokeGameStart() => StartGame?.Invoke();
    public static void InvokeGameOver() => GameOver?.Invoke();
}
