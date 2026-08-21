using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCount : MonoBehaviour
{
    [Header("升级桥接")]
    public UnityEvent<bool> missvalue;
    public ButtonCount button1;
    public ButtonCount button2;

    //升级桥接
    public void MissValue(bool t)
    {
        missvalue?.Invoke(t);
    }
    [HideInInspector]public int count;
    public void countAdd()
    {
        count++;
        Debug.Log("当前个数" + count);
    }
    public void countClear()
    {
        count = 0;
    }
    public void missChange()
    {
        MissValue(count == 1);
        count = 0;
        button1.count = 0;
        button2.count = 0;
    }
    public void missReverse()
    {
        count = 0;
    }
}
