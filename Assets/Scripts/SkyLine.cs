using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLine : MonoBehaviour
{
    [SerializeField] Transform prefab;
    [SerializeField] Vector3 startPosition,minSize,maxSize;
    [SerializeField] int amountCubes=10;
    [SerializeField] float recycleOffset=9f;

    Transform justLink;


    Vector3 nextPosition;

    private Queue<Transform> queue;


    private void Start()
    {
        GameEventManagerRunner.StartGame += StartGame;
        GameEventManagerRunner.GameOver += GameOver;
        queue = new Queue<Transform>(amountCubes);

        nextPosition = startPosition;

        for (int i = 0; i < amountCubes; i++)
        {
            justLink = Instantiate(prefab, new Vector3(0f, 0f, -100f), Quaternion.identity);
            justLink.SetParent(transform);

            queue.Enqueue(justLink);
        }
        enabled = false;
    }

    private void Update()
    {
        if (queue.Peek().localPosition.x + recycleOffset < PlayerMove.distanceTraveled.x)
        {
            Recycle();
        }
    }

    private void Recycle()
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize.x,maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
            );

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        justLink = queue.Dequeue();

        justLink.transform.localPosition = position;
        justLink.localScale = scale;

        nextPosition.x += scale.x;

        queue.Enqueue((justLink));
    }
    private void StartGame()
    {
        nextPosition = startPosition;
        for(int i = 0; i < queue.Count; i++)
        {
            Recycle();
        }
        enabled = true;

    }

    private void GameOver()
    {
        enabled = false;
    }
}
