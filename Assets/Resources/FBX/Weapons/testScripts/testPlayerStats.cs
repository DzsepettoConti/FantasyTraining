using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStats : testCharacterStats
{
    private testPlayerHUD hud;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {
        hud = GetComponent<testPlayerHUD>();

    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Heal(10);
        }
    }

   
}
