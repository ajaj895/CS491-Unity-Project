using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private static Game game;// The game object should only be accessed in the MainGame script.
    private int maxHealth; // The maximum health that the player has.
    private int currentHealth; // The current health of the player.
    private Boolean invuln; // Invulnerability status of the player.
    //private Boolean alive; // If the player is alive or not.
    public Animator animator; // An animator object
    private Health health; 
    public HealthBar healthBar; // A healthbar object
    public PauseMenu menus; // A menus object
    public GameObject player;
    private int difficulty = 0;// A int for difficulty. -1 for easy, 0 for normal/medium, 1 for hard.

    // Start is called before the first frame update
    private void Start()
    {
        init(difficulty);
    }

    public void init(int diff)
    {
        health = GetComponent<Health>();
        checkDifficulty();//Checks and sets difficulty
        //Difficulty section
        if (difficulty == -1) // Easy
        {
            health.setMax(health.getMax() * 2);// Double max health
            health.setCurrent(health.getMax());// Sets current health to max health
        }
        else if (difficulty == 0) // Medium
        {
            health.setMax(health.getMax());
            health.setCurrent(health.getMax());// Sets current health to max health
        }
        else
        {
            health.setMax(health.getMax() / 2);// Half max health, if this is too little health, this may work Convert.ToInt32(health.getMax() / 1.5)
            health.setCurrent(health.getMax());// Sets current health to max health
        }

        maxHealth = health.getMax();
        currentHealth = health.getCurrent();
        healthBar.SetMaxHealth(maxHealth); // Initializes the maximum health of the health bar
        invuln = false;

        print("Player health from player " + maxHealth);
        //alive = true; // Sets alive to true
    }

    // Update is called once per frame
    private void Update()
    {
   
    }

    // TakeDamage reduces the health of the player and updates the healthbar
    public void TakeDamage(int damage)
    {
        //currentHealth -= damage; //The damage is reduced here.
        if (isAlive() && !invuln) //Fixes after death animations
        {
            if (difficulty == -1) //Take half damage
            {
                health.takeDmg(damage / 2);
            }
            else if (difficulty == 0) //Take normal damage
            {
                health.takeDmg(damage);
            }
            else //Take double damage
            {
                health.takeDmg(damage * 2);
            }
            
            healthBar.SetHealth(health.getCurrent()); // Updates the healthbar here.
            animator.SetTrigger("Hurt"); // Shows the hurt animation.
            invuln = true;
            StartCoroutine(WaitForHit());
            
        }

        if (!isAlive()) // Checks if the player's health has gone past the death point.
        {
            Die(); // Starts the end of the game.
        }

    }

    // WaitForAnimation() waits for a given time
    private IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(1); // This is what waits for one second

        invuln = false;


    }


    // Die ends the game when the character has perished
    void Die()
    {
        // Die animation
        animator.SetBool("IsDead", true);
        //alive = false; // Sets player to not alive
        // This coroutine waits for the animation to finish (about a second) before fully ending the game and bringing up the end screen
        StartCoroutine(WaitForAnimation());
    }

    // getHealth() returns the current health of the player
    public int getHealth()
    {
        return currentHealth;
    }
    
    // isAlive() returns if the player is alive is or not
    public Boolean isAlive()
    {
        return health.alive();
    }

    // getMaxHealth() returns what the player's max health is
    public int getMaxHealth()
    {
        return maxHealth;
    }

    // WaitForAnimation() waits for a given time
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1); // This is what waits for one second

        //Temporary end as normal isn't working with this player health
        menus.End();
    }

    //Checks the difficulty and sets the difficulty
    private void checkDifficulty()
    {
        if (!File.Exists("diff.txt"))//Creates the diff file if haven't been created.
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
        }
        else
        {
            string inDiff = File.ReadAllText("diff.txt");
            int diff;
            bool succ = int.TryParse(inDiff, out diff);
            if (succ)
            {
                if (diff > 1 || diff < -1) //If Difficulty got set in the text file more or less than what's available.
                {
                    print("Difficulty out of bounds, set to normal");//For log/debugging
                    difficulty = 0; //Sets the difficulty to medium
                    File.WriteAllText("diff.txt", difficulty.ToString());
                    print("Difficulty set to " + difficulty.ToString());//For log/debugging
                }
                else //only if everything has gone right.
                {
                    difficulty = diff;
                }
            }
            else
            {
                File.WriteAllText("diff.txt", difficulty.ToString());
            }
            //File.WriteAllText("diff.txt", difficulty.ToString());
            //print("Set difficulty to " + difficulty.ToString());
        }
    }
}
