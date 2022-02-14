using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBlock : MonoBehaviour
{

    public Transform[] blockPrefabs;
    public float roomSize = 5;

    void Start()
    {

    }

    public void InitBlock(BlockType type)
    {

        // pick random prefabs:
        Transform prefabN = blockPrefabs[Random.Range(0, blockPrefabs.Length)];

        // pick positions:
        float dis = roomSize / 2 - 0.25f;

        Vector3 posN = new Vector3(0, 0, +dis) + transform.position;

        // pick rotation:
        Quaternion rotN = Quaternion.Euler(0, ((Random.Range(0, 100) < 50) ? 0 : 180), 0);

        // spawn walls
        Instantiate(prefabN, posN, rotN, transform);

   }

}
