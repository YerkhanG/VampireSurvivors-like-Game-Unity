using System.Collections.Generic;
using UnityEngine;


public class PropsRandomizer : MonoBehaviour
{
    public List<GameObject> propsSpawnPoints;
    public List<GameObject> propsPrefabs;

    void Start()
    {
        SpawnProps();   
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propsSpawnPoints)
        {
            int randomNumber = Random.Range(0, propsPrefabs.Count);
            GameObject prop = Instantiate(propsPrefabs[randomNumber], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
