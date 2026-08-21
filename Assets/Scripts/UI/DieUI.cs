using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
{
    public UnityEvent<bool> WASD;
    public UnityEvent<bool> ESC;
    private TMP_Text score;
    public UIinfo ui;

    private void Awake()
    {
        TMP_Text[] allText = GetComponentsInChildren<TMP_Text>(true);
        foreach (var text in allText)
        {
            if (text.CompareTag("score"))
            { 
                score = text;
                return;
            }
        }
        return;
    }
    private void OnEnable()
    {
        WASD?.Invoke(false);
        ESC?.Invoke(false);
        Time.timeScale = 0;
        score.text = "" + ui.scorecount;
    }
    public void Dieui()
    { 
        gameObject.SetActive(true);
    }

    public void reStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void GameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}
