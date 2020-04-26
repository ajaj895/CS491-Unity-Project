using System.Collections;
using System.Collections.Generic;
using UnityEngine;//Always needed
using UnityEngine.SceneManagement;//Needed for changing scenes and scene work

public class MainMenu : MonoBehaviour
{
    public void StartGame()//Starts game (Actually goes to the next scene in the build which is the Game scene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Goes to the next scene in the build order (MAKE SURE BUILD ORDER IS CORRECT)
    }

    public void QuitGame()//Quits game outside of the unity editor (Note: does not do anything in the editor)
    {
        Debug.Log("Quiting Game.");//For debug purposes in the editor
        Application.Quit();
    }
}
