using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    public void StartSave()
    { 
        SaveManager.instance.SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
