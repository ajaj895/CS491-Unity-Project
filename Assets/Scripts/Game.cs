using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;

public class Game : MonoBehaviour
{
    private protected byte gameState;//Game state variable, 0 = not initialized, 1 = running, 2 = game is paused, 3 = game has ended
    private ulong score; //Game score, an unsigned long (can't be a negative value)
    private DateTime startT; //A dateTime object representing the start time
    private DateTime endT; //A dateTime object representing the end time
    private TimeSpan runT; //A TimeSpan object representing the span of time between start to end times
    private byte difficulty;

    private string[] keybinds;//In order of left|right|jump|crouch|attack|...etc
    
    // Start is called before the first frame update
    public Game()//initiates the game.
    {
        startGame();
    }

    public void startGame()
    {
        initGame();
    }

    private void initGame()//Sets the score to 0, sets the start time, reads and sets the keybinds, and sets the gamestate to running.
    {
        score = 0;
        setStartTime();
        keybinds = File.ReadAllLines("binds.txt");

        setGameState(1);
    }

    // Update is called once per frame

    public void pause()//Sets the gamestate to paused (2).
    {
        pauseGame();
    }

    public void resume()//Sets the gamestate to resumed/running (1).
    {
        resumeGame();
    }

    public void end()//Sets the game to ended along with setting the endtime and runtime.
    {
        endGame();
    }

    private void pauseGame()//Sets the gamestate to paused (2).
    {
        setGameState(2);
    }

    private void resumeGame()//Sets the gamestate to resumed/running (1).
    {
        setGameState(1);
    }

    private void endGame()//Ends the game by setting the gamestate to 3, sets the endtime, and sets the runtime
    {
        setEndTime();
        setRunTime();
        setGameState(3);
    }

    protected void addScore(uint toAdd)//Adds to the score
    {
        score = score + toAdd;
    }

    public ulong getScore()//returns the score as an unsigned int
    {
        return score;
    }

    private byte getGameState()//returns the gamestate.
    {
        return gameState;
    }

    private protected void setGameState(byte chg)//Sets the gamestate.
    {
        gameState = chg;
    }

    public Boolean isRunning()//Returns boolean if the game is running.
    {
        return (getGameState() == 1);
    }

    public Boolean isPaused()//Returns boolean if the game is paused.
    {
        return (getGameState() == 2);
    }

    public Boolean isEnded()//Returns boolean if the game has ended.
    {
        return (getGameState() == 3);
    }

    public DateTime getStartTime()//Returns startT. 
    {
        return startT;
    }

    private void setStartTime()//Sets start time to the current time.
    {
        startT = DateTime.Now;
    }

    public DateTime getEndTime()//Returns endT if game has ended, if game hasn't ended, it ends the game.
    {
        if(isEnded())
        {
            return endT;
        } 
        else
        {
            end();
            return endT;
        }
            
    }

    private void setEndTime()//Sets end time to the current time
    {
        endT = DateTime.Now;
    }

    public TimeSpan getRunTime()//Returns runT if game has ended, if game hasn't ended, it ends the game.
    {
        if (isEnded())
        {
            return runT;
        }
        else
        {
            end();
            return runT;
        }
        
    }

    private void setRunTime()//Sets run time to the end time - start time
    {
        runT = endT - startT;
    }

    protected void setDifficulty(byte diff)//sets the difficulty to the inputted difficulty
    {
        difficulty = diff;
    }

    public byte getDifficulty()//Returns the difficulty
    {
        return difficulty;
    }
    
    // -- keybind section --
    
    public KeyCode getLeftBind()//This could be made more effecient by saving the keybinds instead of parsing them every time in the future.
    {
        return getKeybind(0);//array location 0
    }

    public KeyCode getRightBind()
    {
        return getKeybind(1);//array location 1
    }

    public KeyCode getJumpBind()
    {
        return getKeybind(2);//array location 2
    }

    public KeyCode getCrouchBind()
    {
        return getKeybind(3);//array location 3
    }

    public KeyCode getAttackBind()
    {
        return getKeybind(4);//array location 4
    }

    private KeyCode getKeybind(int arrLoc)//returns the keycode of the inputted array location
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keybinds[arrLoc]);//converts the string to the typecode.
    }
}
