using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnCoins : MonoBehaviour
{

    [SerializeField] private GameObject[] coins;
    [SerializeField] private Vector2 limits;
    [SerializeField] private float y = 1f;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float rate = 1f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();
        InvokeRepeating("SpawnCoin",
            startDelay, rate);
    }

    void SpawnCoin()
    {
        int random= Random.Range(0, coins.Length);
        float x = Random.Range(-limits.x, limits.x);
        float z = Random.Range(-limits.y, limits.y);
        Vector3 SpawnPos = new Vector3(x, y, z);
        Instantiate(coins[random], SpawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.stop)
        {
            CancelInvoke("SpawnCoin");
        }
    }
}
