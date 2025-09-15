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

    private Vector3 lastCheckedPosition;

    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        lastCheckedPosition = Player.transform.position;
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
        
        if (Vector3.Distance(Player.transform.position, lastCheckedPosition) < checkerRadius / 2f)
        {
            return;
        }
        
        lastCheckedPosition = Player.transform.position;
       
        CheckDirection("Right");
        CheckDirection("Left");
        CheckDirection("Up");
        CheckDirection("Down");
        CheckDirection("Right up");
        CheckDirection("Right down");
        CheckDirection("Left up");
        CheckDirection("Left down");
    }
    
    void CheckDirection(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(direction).position;
            SpawnChunk();
        }
    }
    
    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
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
    public void SetCurrentChunk(GameObject chunk)
    {
        currentChunk = chunk;
    }
}