using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Spawners")]
    public SkeletonSpawner skeletonSpawner;

    public void SetWave(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                skeletonSpawner.SpawningEnabled = true;
                skeletonSpawner.SpawnRateMultiplier = 1f;
                break;
            case 5:
                skeletonSpawner.SpawnRateMultiplier = 2f;
                break;
            case 10:
                skeletonSpawner.SpawningEnabled = false;
                break;
        }
    }
}