using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] protected Button btn;

    protected virtual void Awake()
    {
        if (btn == null)
            btn = GetComponent<Button>();

        btn.onClick.AddListener(OnButtonClick);
    }
    protected abstract void OnButtonClick();

    //切换场景
    protected void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //设置状态
    protected void SetButtonInteractable(bool state)
    {
        btn.interactable = state;
    }
}
