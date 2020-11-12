using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;


public class Health : MonoBehaviour
{
    // TODO: Make a custom health editor using UnityEditor in another script.
    public int maxHealth;
    public int currentHealth;
    public Boolean randomizedHealth = false;
    public int max; // necessary only if randomizedHealth is true
    public int min; // necessary only if randomizedHealth is true
    //private int difficulty;// For difficulty
    //private Boolean isAlive;



    // Start is called before the first frame update
    void Start()
    {
        //Checks for randomized health enabled or not along with making sure the max isn't below the min, and if neither are zero
        if(randomizedHealth && max != 0 && min != 0 && max > min)
        {
            maxHealth = UnityEngine.Random.Range(min, max);
            currentHealth = maxHealth;
        }
    }

    // Returns the max health of the creature
    public int getMax()
    {
        return maxHealth;
    }

    // Sets the max health, this should be protected in some way but at the moment, this will have to do.
    public void setMax(int mx)
    {
        maxHealth = mx;
    }

    //Sets the current health of the player
    public void setCurrent(int cur)
    {
        currentHealth = cur;
    }

    // Gets the current health of the creature
    public int getCurrent()
    {
        return currentHealth;
    }

    // Returns the living state of the creature
    public Boolean alive()
    {
        return (currentHealth > 0);
    }

    // Reduces the health of a creature
    public void takeDmg(int dmg)
    {
        currentHealth -= dmg;
    }


    // Heals the creature, if the sum of heal and current health is greater than max health, the current health gets set to the max health
    public void heal(int heal)
    {
        if ((heal + currentHealth) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }

    
}
