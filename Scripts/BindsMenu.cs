using System.IO;// For File system
using TMPro;// For TextMaker Pro
using UnityEngine;// Always needed for unity

public class BindsMenu : MonoBehaviour // NOTE: To access buttons, you have to use the children of this object because this is attached to Binds canvas in the keybinds menu.
{
    // Strings for the File reader
    private string leftBind;
    private string rightBind;
    private string jumpBind;
    private string crouchBind;
    private string attackBind;
    // We could add more binds at a later date
    // This isn't the best solution because you would have to add new strings and Game objects and modify the loops in the bind method everytime that you want to add a new key.

    // String arrays for saving the keys
    private string[] defaultKeys = { "a", "d", "w", "s", "f" };
    private string[] savedLines; //Lines from the file
    private string[] tempLines; //Lines held in memory until saveKey() is called

    // Button objects
    private GameObject leftBut;
    private GameObject rightBut;
    private GameObject jumpBut;
    private GameObject crouchBut;
    private GameObject attackBut;
    
    // Start is called before the first frame update
    void Start()
    {

        initButtons();//Gets GameObjects
        print(leftBut + "\n" + rightBut + "\n" + jumpBut + "\n" + crouchBut + "\n" + attackBut + "\n");//testing purposes
        if (File.Exists("binds.txt"))//If binds has been created before
        {
            savedLines = File.ReadAllLines("binds.txt");
            for (int i = 0; i < savedLines.Length; i++)
            {
                if (i == 0)
                {
                    leftBind = savedLines[i];
                    
                }
                else if (i == 1)
                {
                    rightBind = savedLines[i];
                    
                }
                else if (i == 2)
                {
                    jumpBind = savedLines[i];
                    
                }
                else if (i == 3)
                {
                    crouchBind = savedLines[i];
                    
                }
                else if (i == 4)
                {
                    attackBind = savedLines[i];
                   
                }
                else print("Unknown line number.");
            }
        }
        else
        {
            string[] saveLines = { "a", "d", "w", "s", "f" };
            
            //The key binds are in order of the list of keys on the keyBinds settings menu
            //That being: left, right, jump, crouch, attack
            //File.WriteAllText("binds.txt", defaultBinds);
            //Keybind setter
            leftBind = "a";
            rightBind = "d";
            jumpBind = "w";
            crouchBind = "s";
            attackBind = "f";
        }
        //print(leftBut.transform.GetChild(0));//for testing purposes
        setTemp();//sets temp to saveLines
        //Button text setting
        setText(leftBut, leftBind);
        setText(rightBut, rightBind);
        setText(jumpBut, jumpBind);
        setText(crouchBut, crouchBind);
        setText(attackBut, attackBind);
    }

    void setTemp()
    {
        int count = 0;
        tempLines = new string[savedLines.Length];
        foreach(string s in savedLines)
        {
            tempLines[count] = savedLines[count];
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //--BUTTON ACTION AREA--
    public void leftPressed()//This method listens for a keypress and sets it to the array and to the button's text
    {
        setText(leftBut, "Press any key...");
        KeyCode newKey;
        print("left");
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))// The following forum post was an inspiration for this solution: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        {
            
            if (Input.GetKey(key))
            {
                newKey = key;
                setText(leftBut, newKey.ToString());
                tempLines[0] = newKey.ToString();
                //print(newKey);
                //foreach (string s in lines) print(s);//for debugging
                break;
            }
        }
    }
    public void rightPressed()
    {
        setText(rightBut, "Press any key...");
        KeyCode newKey;
        print("right");
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))// The following forum post was an inspiration for this solution: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        {
            
            if (Input.GetKey(key))
            {
                newKey = key;
                setText(rightBut, newKey.ToString());
                tempLines[1] = newKey.ToString();
            }
        }
    }
    public void jumpPressed()
    {
        setText(jumpBut, "Press any key...");
        KeyCode newKey;
        print("jump");
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))// The following forum post was an inspiration for this solution: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        {
            
            if (Input.GetKey(key))
            {
                newKey = key;
                setText(jumpBut, newKey.ToString());
                tempLines[2] = newKey.ToString();
            }
        }
    }
    public void crouchPressed()
    {
        setText(crouchBut, "Press any key...");
        KeyCode newKey;
        print("crouch");
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))// The following forum post was an inspiration for this solution: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        {
            
            if (Input.GetKey(key))
            {
                newKey = key;
                setText(crouchBut, newKey.ToString());
                tempLines[3] = newKey.ToString();
            }
        }
    }
    public void attackPressed()
    {
        setText(attackBut, "Press any key...");
        KeyCode newKey;
        print("attack");
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))// The following forum post was an inspiration for this solution: https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/
        {
            
            if (Input.GetKey(key))
            {
                newKey = key;
                setText(attackBut, newKey.ToString());
                tempLines[4] = newKey.ToString();
            }
        }
    }

    public void saveKeys() //Saves keybinds to binds.txt
    {
        string formatKeys = "";
        foreach (string s in tempLines)//Better solution for adding keybinds in the future.
        {
            formatKeys = formatKeys + s + "\n";
        }
        //print(formatKeys);
        savedLines = tempLines; // Sets saved lines equal to temp lines
        File.WriteAllText("binds.txt", formatKeys);
        print("Saved keybinds!");
    }

    public void resetToDefault() // Resets the binds.txt and binds to defaultKeys
    {   
        tempLines = defaultKeys;
        saveKeys();
        updateButtons();
    }

    public void updateButtons() //Updates the text of the buttons when the back button is pressed, if the binds aren't saved, it will returned to default
    {
        setText(leftBut, savedLines[0]);
        setText(rightBut, savedLines[1]);
        setText(jumpBut, savedLines[2]);
        setText(crouchBut, savedLines[3]);
        setText(attackBut, savedLines[4]);
    }

    void initButtons() //Passes in the button objects to the variables
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //print("For loop: " + transform.GetChild(i));
            if (transform.GetChild(i).name.Equals("leftBind", System.StringComparison.InvariantCultureIgnoreCase))
            {
                leftBut = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("rightBind", System.StringComparison.InvariantCultureIgnoreCase))
            {
                rightBut = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("jumpBind", System.StringComparison.InvariantCultureIgnoreCase))
            {
                jumpBut = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("crouchBind", System.StringComparison.InvariantCultureIgnoreCase))
            {
                crouchBut = transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("attackBind", System.StringComparison.InvariantCultureIgnoreCase))
            {
                attackBut = transform.GetChild(i).gameObject;
            }
            else
            {
                print("Unknown button");
            }
        }
    }

    void setText(GameObject button, string txt)//sets button text
    {
        button.GetComponentInChildren<TMP_Text>().text = txt;
    }
}
