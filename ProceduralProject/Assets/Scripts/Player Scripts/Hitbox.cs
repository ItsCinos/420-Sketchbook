using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damageAmount = 20;

    
    public void OnTriggerEnter(Collider other)
    {
        HealthSystem health = other.GetComponent<HealthSystem>();
        TopDownPlayerMovement player = other.GetComponent<TopDownPlayerMovement>();

        if (health && player)
        {
            health.TakeDamage(damageAmount);
            SoundEffectBoard.PlayPunch();
        }

        

    }
}
