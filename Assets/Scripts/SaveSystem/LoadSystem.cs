using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    private void Start()
    {
        SaveManager.instance.GetRef();
        if (SaveManager.instance.dudang)
        { 
            SaveManager.instance.LoadGame();
            SaveManager.instance.dudang = false;
        }
    }
}
