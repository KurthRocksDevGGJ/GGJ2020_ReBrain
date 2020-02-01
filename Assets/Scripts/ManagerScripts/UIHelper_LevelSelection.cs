using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIHelper_LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("" + Application.dataPath);
        //string[] filePaths = Directory.GetFiles(Application.dataPath + "/", "*.anim");
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/Animations", "*.anim");
        foreach (string file in filePaths) {
            Debug.Log(file);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
