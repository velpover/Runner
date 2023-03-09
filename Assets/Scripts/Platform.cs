using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Material[] _materials;
    [SerializeField] PhysicMaterial[] _physicsMaterials;
    [SerializeField] Transform prefab;

    [SerializeField] Vector3 startPosition, minSize, maxSize,minGap,maxGap;
    [SerializeField] int amountCubes = 10;
    [SerializeField] float recycleOffset = 9f,minY,maxY;

    [SerializeField] Booster boost;

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
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
            );

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        boost.SpawnIfAvailable(position);

        justLink = queue.Dequeue();

        int randomIndex = Random.Range(0, _materials.Length);

        justLink.GetComponent<Renderer>().material = _materials[randomIndex];
        justLink.GetComponent<Collider>().material = _physicsMaterials[randomIndex];
        justLink.transform.localPosition = position;
        justLink.localScale = scale;

        nextPosition += new Vector3(
             Random.Range(minGap.x, maxGap.x) + scale.x,
             Random.Range(minGap.y, maxGap.y),
             Random.Range(minGap.z, maxGap.z));

        if (nextPosition.y < minY)
        {
            nextPosition.y = minY + maxGap.y;
        }
        else if (nextPosition.y > maxY)
        {
            nextPosition.y = maxY - maxGap.y;
        }

        queue.Enqueue((justLink));
    }

    private void StartGame()
    {   
        nextPosition = startPosition;
        for (int i = 0; i < amountCubes; i++)
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
