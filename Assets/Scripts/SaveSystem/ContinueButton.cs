using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    //继续游戏按钮
    public Button continueBtn;

    private void Start()
    {
        //继续游戏按钮检测
        if (SaveManager.instance.Saved() && continueBtn != null)
        {
            continueBtn.interactable = true;
            Image btnImg = continueBtn.GetComponent<Image>();
            if (btnImg != null)
            {
                Color c = btnImg.color;
                c.r = 255;
                c.g = 255;
                c.b = 255;
                c.a = 255;
                btnImg.color = c;
            }
        }
        else
        {
            continueBtn.interactable = false;
            Image btnImg = continueBtn.GetComponent<Image>();
            if (btnImg != null)
            {
                Color c = btnImg.color;
                c.r = 122;
                c.g = 122;
                c.b = 122;
                c.a = 207;
                btnImg.color = c;
            }
        }
    }

    //读档
    public void StartLoad()
    { 
        SaveManager.instance.StartLoad();
    }
}
