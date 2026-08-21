using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public UnityEvent<bool> WASD;
    public void Stop()
    { 
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueButton()
    { 
        gameObject.SetActive(false);
        Time.timeScale = 1;
        WASD?.Invoke(true);
    }
    public void BreakButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
}
