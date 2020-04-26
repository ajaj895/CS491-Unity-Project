using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    private Boolean alive;

    public HealthBar healthBar;
    // Start is called before the first frame update
    /*
    public Player()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        alive = true;
    }
    */
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        alive = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
            //print(isAlive());
            //Debug.Log(isAlive());
        }
        
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < 1) alive =  false;
    }

    public int getHealth()
    {
        return currentHealth;
    }
    
    public Boolean isAlive()
    {
        return alive;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
