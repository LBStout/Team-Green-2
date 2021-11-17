using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public MultiSceneConfig Config;
    // Start is called before the first frame update
    void Start()
    {

            for (int i = 1; i <= Config.Scenes.Length; i++)
            {
                SceneManager.LoadScene(Config.Scenes[i].path, LoadSceneMode.Additive);
            }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
