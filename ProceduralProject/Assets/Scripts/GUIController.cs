using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIController : MonoBehaviour
{
    public Text numAmmo;
    public Text playerHealthDisplay;

    private int numAmmoMax = 0;

    public PlayerWeapon pw;
    public HealthSystem playerHealth;


    void Start()
    {        
    }


    void Update()
    {
        numAmmo.text = pw.roundsInClip + "  ";
        playerHealthDisplay.text = playerHealth.health + "%";
        
    }
}