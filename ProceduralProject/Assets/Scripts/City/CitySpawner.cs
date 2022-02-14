using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{

    public CityBlock prefabSky;

    [Range(4, 25)]
    public int citySize = 10;

    [Range(1, 100)]
    public int spaceBetweenRooms = 5;


    void Start()
    {
        // spawn a CityLayout
        CityLayout city = new CityLayout();
        city.Generate(citySize);

        int[,] blocks = city.GetBlocks();

        // loop through rooms, spawn prefabs
        for (int x = 0; x < blocks.GetLength(0); x++)
        {
            for (int z = 0; z < blocks.GetLength(1); z++)
            {

                if (blocks[x, z] > 0) continue; // skip room

                Vector3 pos = new Vector3(x, 0, z) * spaceBetweenRooms;
                CityBlock newBlock = Instantiate(prefabSky, pos, Quaternion.identity);

                newBlock.InitBlock((BlockType)blocks[x, z]);

            }
        }


    } // ends Start()


} // ends CitySpawner
