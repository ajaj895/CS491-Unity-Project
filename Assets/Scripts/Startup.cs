using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists("binds.txt"))//Creates the binds file if haven't been created.
        {
            string formatKeys = "a\nd\nw\ns\nf";
            File.WriteAllText("binds.txt", formatKeys);
        }
    }
}
