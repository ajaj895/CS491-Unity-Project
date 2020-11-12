using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;// For TextMaker Pro


public class PauseMenu : MonoBehaviour
{
    private static Game game;// The game object should only be accessed in the MainGame script.
    private static Player player;// The player object should only be accessed in the MainGame script.

    public bool GameIsPaused = false; // Checks if game is paused from the MainGame Script (using the game class)
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject endMenuUI;
    
    void Start()
    {
        init(new Game(0), new Player()); //This is only temporary! If you are going to use pause menus, call init() from your game class.
    }
    
    //initiates the menus
    public void init(Game gm, Player play) //This is called from MainGame class.
    {
        game = gm;
        player = play;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        //if (player.getHealth() < 0) End();// Ends the game
    }
    public void Resume()
    {
        if (game.isPaused())// Checks to see if the game is running (you can't pause a game over screen)
        {
            pauseMenuUI.SetActive(false);
            gameUI.SetActive(true);
            Time.timeScale = 1f;
            game.resume();
        }
    }
    void Pause()
    {
        if (game.isRunning())// Checks to see if the game is running (you can't pause a game over screen)
        {
            pauseMenuUI.SetActive(true);
            gameUI.SetActive(false);
            Time.timeScale = 0f;
            game.pause();
            //print("Player health from menus" + player.getMaxHealth());
        }
    }

    public void End()
    {
        game.end();
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(false);
        GameObject endscores = endMenuUI.transform.GetChild(3).gameObject;
        endscores.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = game.getScore().ToString();//sets the score area
        endscores.transform.GetChild(1).transform.GetChild(0).gameObject.gameObject.GetComponentInChildren<TMP_Text>().text = game.getRunTime().ToString();//These two can be made less complex (gets the child of the child)
        endMenuUI.SetActive(true);
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }

    public void RestartGame()
    {
        Restart();
        Time.timeScale = 1.0f;
        //GameIsPaused = false;
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
