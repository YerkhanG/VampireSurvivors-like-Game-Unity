using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject Player;
    public float checkerRadius;
    public GameObject currentChunk;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    PlayerMovement playerMovement;


    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist;

    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
        
    }

    void ChunkChecker()
    {
        if(!currentChunk)
        {
            return;
        }
        if (playerMovement.moveInput.x > 0 && playerMovement.moveInput.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;  //Right
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.x < 0 && playerMovement.moveInput.y == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;    //Left
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.y > 0 && playerMovement.moveInput.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position; //Up
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.y < 0 && playerMovement.moveInput.x == 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;    //Down
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.x > 0 && playerMovement.moveInput.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right up").position;   //Right up
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.x > 0 && playerMovement.moveInput.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right down").position;  //Right down
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.x < 0 && playerMovement.moveInput.y > 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left up").position;  //Left up
                SpawnChunk();
            }
        }
        else if (playerMovement.moveInput.x < 0 && playerMovement.moveInput.y < 0)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left down").position; //Left down
                SpawnChunk();
            }
        }
    }
    
    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk =Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;   //Check every 1 second to save cost, change this value to lower to check more times
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(Player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}