using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Loader : MonoBehaviour
{
    //public System.Action<bool> NextLevel;  // true - next level, false - another try;

    static UI_Loader UI;
    void Awake()
    {
        if (UI == null)
        {
            UI = this;
            if (SceneManager.GetSceneByName("UI").isLoaded == false )
            {
                SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);

            }
        }
        
        
            
        
        
    }
}
