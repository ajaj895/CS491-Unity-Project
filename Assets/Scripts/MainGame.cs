using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainGame : MonoBehaviour
{
    public static Game thisGame;
    //private int health;
    public PauseMenu menus;
    private static int difficulty = 0;
    public static Player thisPlayer = new Player();

    // Start is called before the first frame update. This is also the initialization phase.
    void Start()
    {
        //health = thisPlayer.getMaxHealth();
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
                if(diff > 1 || diff < -1) //If Difficulty got set in the text file more or less than what's available.
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
        thisGame = new Game(difficulty);//Makes the game 
        thisGame.startGame(difficulty);//Starts the game
        thisPlayer.init(difficulty);
        menus.init(thisGame, thisPlayer);//Initializes the menus
        print(thisGame.isRunning());//for testing purposes
        
        //thisGame = new Game();
        //thisPlayer = new Player();

    }

    // Update is called once per frame
    void Update()
    {
        //print(health);
        if (thisGame.isRunning() && thisPlayer.getHealth() < 1)
        {
            //thisPlayer.TakeDamage(20); doesn't work at the moment
            //--TEMPORARY SOLUTION--
            //health = health - 20;
            //menus.End();
            

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
