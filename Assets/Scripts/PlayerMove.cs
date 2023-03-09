using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static Vector3 distanceTraveled;
    
    [SerializeField] Vector3 jumpVelocity,boosterVelocity;
    [SerializeField] float acceleration;
    [SerializeField] InputEv inputSpace;

    bool isGround;
    private static int boosts = 0;
    float gameOverY = -6f;
    Vector3 startPosition;

    Rigidbody body;
    Renderer render;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();

    }
    private void Start()
    {
        startPosition = transform.localPosition;

        GameEventManagerRunner.StartGame += StartGame;
        GameEventManagerRunner.GameOver += GameOver;

        startPosition = transform.localPosition;
        render.enabled = false;
        body.isKinematic = true;
        enabled = false;
    }
    private void OnEnable()
    {
        inputSpace.inputSpace += Jump;
    }
    private void OnDisable()
    {
        inputSpace.inputSpace -= Jump;
    }
    private void Update()
    {
        distanceTraveled.x = transform.localPosition.x;

        UILogic.SetScore(distanceTraveled.x);

        if (transform.localPosition.y < gameOverY)
        {
            GameEventManagerRunner.InvokeGameOver();
        }
    }

    void FixedUpdate()
    {
        if (isGround)
        {
            body.AddForce(acceleration,0,0,ForceMode.Acceleration);
        }
        
    }

    private void OnCollisionEnter()
    {
        isGround = true;
    }

    private void OnCollisionExit()
    {
        isGround= false;
    }

    private void Jump()
    {
        if (isGround)
        {
            body.AddForce(jumpVelocity, ForceMode.VelocityChange);
            isGround = false;
        }
        else if (boosts > 0)
        {
            body.AddForce(boosterVelocity, ForceMode.VelocityChange);

            boosts--;
            UILogic.SetBoost(boosts);
        }
    }

    private void StartGame()
    {
        boosts = 0;
        UILogic.SetBoost(boosts);

        distanceTraveled = Vector3.zero;
        UILogic.SetScore(distanceTraveled.x);

        transform.localPosition = startPosition;
        render.enabled = true;
        body.isKinematic = false;
        enabled = true;
    }

    private void GameOver()
    {
        render.enabled=false;
        body.isKinematic=true;
        enabled = false;
    }

    public static void AddBoost()
    {
        boosts++;
        UILogic.SetBoost(boosts);
    }
}
