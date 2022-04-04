using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public Transform zombie;

    private float spawnCooldown = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnCooldown > 0) spawnCooldown -= Time.deltaTime;

        SpawnZombie();

    }

    private void SpawnZombie()
    {
        if (spawnCooldown > 0) return; // wait longer to spawn again...

        Transform z = Instantiate(zombie, transform.position, Quaternion.identity);

        spawnCooldown = 7;
    }
}
