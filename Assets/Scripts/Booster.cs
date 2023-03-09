using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] Vector3 offset, rotationVelocity;
    [SerializeField] float recycleOffset, spawnChance;

    private void Start()
    {
        GameEventManagerRunner.GameOver += GameOver;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(transform.position.x + recycleOffset < PlayerMove.distanceTraveled.x)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.Rotate(rotationVelocity * Time.deltaTime);
    }
    public void SpawnIfAvailable(Vector3 position)
    {
        if (gameObject.activeSelf || spawnChance <= Random.Range(0, 100))
        {
            return;
        }
        transform.localPosition = position+offset;
        gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove.AddBoost();
        gameObject.SetActive(false);
    }
}
