using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class UILogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instuction, runner, gameOver,boostText,scoreText;

    private static UILogic instance;

    private void Start()
    {
        instance = this;
    }
    void OnEnable()
    {
        gameOver.enabled = false;

        GameEventManagerRunner.StartGame += GameStartTextChange;
        GameEventManagerRunner.GameOver += GameOverTextChange;

    }
    private void OnDisable()
    {
        GameEventManagerRunner.StartGame -= GameStartTextChange;
        GameEventManagerRunner.GameOver -= GameOverTextChange;
    }

    private void GameStartTextChange()
    {
        gameOver.enabled = false;
        instuction.enabled = false;
        runner.enabled = false;
    }

    private void GameOverTextChange()
    {
        gameOver.enabled = true;
        instuction.enabled = true;
    }

    public static void SetScore(float distance)
    {
        instance.scoreText.SetText("Score : "+distance.ToString("f0"));
    }

    public static void SetBoost(int boost)
    {
        instance.boostText.SetText(": "+boost.ToString());
    }
}
