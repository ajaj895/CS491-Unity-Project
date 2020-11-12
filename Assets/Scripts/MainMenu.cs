using System.Collections;
using System.Collections.Generic;
using System.IO;//Needed for the file
using UnityEngine;//Always needed
using UnityEngine.SceneManagement;//Needed for changing scenes and scene work

public class MainMenu : MonoBehaviour
{

    int difficulty = 0;//Starts at normal -1 easy, 0 normal, 1 hard.

    
    public void StartGame()//Starts game (Actually goes to the next scene in the build which is the Game scene)
    {
        if (!File.Exists("diff.txt"))//Creates the diff file if haven't been created.
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
        }
        else
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
            print("Set difficulty to " + difficulty.ToString());
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Goes to the next scene in the build order (MAKE SURE BUILD ORDER IS CORRECT)
    }

    public void SetDifficulty(int diff)//After start game is clicked, this brings up the difficulty selection
    {
        difficulty = diff;
        if (!File.Exists("diff.txt"))//Creates the diff file if haven't been created.
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
        }
        else
        {
            File.WriteAllText("diff.txt", difficulty.ToString());
            print("Set difficulty to " + difficulty.ToString());
        }
    }

    public void QuitGame()//Quits game outside of the unity editor (Note: does not do anything in the editor)
    {
        Debug.Log("Quiting Game.");//For debug purposes in the editor
        Application.Quit();
    }
}
