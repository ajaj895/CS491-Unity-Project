using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainGame : MonoBehaviour // : MonoBehaviour
{
    public static Game thisGame = new Game();
    public static Player thisPlayer = new Player();
    private int health;
    public PauseMenu menus;
    // Start is called before the first frame update
    void Start()
    {
        health = thisPlayer.getMaxHealth();
        thisGame.startGame();
        print(thisGame.isRunning());//for testing purposes
        
        //thisGame = new Game();
        //thisPlayer = new Player();

    }

    // Update is called once per frame
    void Update()
    {
        //print(health);
        if (Input.GetKeyDown(KeyCode.Space) && thisGame.isRunning())
        {
            //thisPlayer.TakeDamage(20); doesn't work at the moment
            //--TEMPORARY SOLUTION--
            health = health - 20;
            if(health < 1)
            {
                print("dead");
                menus.End();
            }

        }
        
        /*
        if (!thisPlayer.isAlive())//This doesn't work because the player class is not an object.
        {
            //print("dead");
        }
        */
        //print(thisGame.isRunning());
    }
}
