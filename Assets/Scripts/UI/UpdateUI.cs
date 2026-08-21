using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class UpdateUI : MonoBehaviour
{
    //获取玩家信息
    public CharacterBase player;

    private List<GameObject> choiceItem = new List<GameObject>();
    private TMP_Text yue;

    //紊乱值
    public float missValue = 0;
    private Slider missValuer;

    //禁用输入
    public UnityEvent<bool> WASD;
    public UnityEvent<bool> ESC;

    //升级退出按钮
    public ButtonCount btn1;
    public ButtonCount btn2;
    public ButtonCount btn3;

    private void Awake()
    {
        Transform parent = this.transform;

        foreach (Transform child in parent)
        {
            if (child.CompareTag("choice"))
            {
                GameObject item = child.gameObject;
                choiceItem.Add(item);
            }
            if (child.CompareTag("moneyCount"))
            {
                yue = FindText("qian", child.gameObject);
            }
            if (child.CompareTag("MissValue"))
            {
                missValuer = child.GetComponent<Slider>();
                missValuer.maxValue = 100;
                missValuer.minValue = 0;
            }
        }

    }
    private void OnEnable()
    {
        WASD?.Invoke(false);
        ESC?.Invoke(false);
        Time.timeScale = 0;
        foreach (GameObject item in choiceItem)
        { 
            missValuer.value = missValue;
            GoodUI(item);
            float rd = Random.Range(0,100);
            if (rd <= missValue)
            {
                BadUI(item);
                //Debug.Log("随机:" + rd + "miss:" + missValue);
            }
        }
        yue.text = "" + player.money;

    }

    private void OnDisable()
    {
        WASD?.Invoke(true);
        ESC?.Invoke(true);
        Time.timeScale = 1;
    }
    //查询函数-文字
    public TMP_Text FindText(string name,GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (child.CompareTag(name))
            {
                TMP_Text txt = child.GetComponent<TMP_Text>();
                return txt;
            }
        }
        return null;
    }
    //查询函数-购买按钮
    public Button FindButton(string name,GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (child.CompareTag(name))
            {
                Button btn = child.GetComponent<Button>();
                return btn;
            }
        }
        return null;
    }

    //紊乱值变动
    public void MissValueChange(bool y)
    {
        if (y)
        {
            missValue += 20;
        }
        else
        {
            missValue -= 10;
        }    
    }

    //生成好词条
    private void GoodUI(GameObject obj)
    {
        int i = Random.Range(0, 10);
        switch (i)
        {
            case 0:
                AtkUpdate ATK = new AtkUpdate(true,player);
                ATK.ui(FindText("goodNew",obj));
                ATK.badRefrash(FindText("badNew", obj));
                ATK.Btn(FindButton("buy",obj));
                return;
            case 1:
                SpdUpdate SPD = new SpdUpdate(true, player);
                SPD.ui(FindText("goodNew", obj));
                SPD.badRefrash(FindText("badNew", obj));
                SPD.Btn(FindButton("buy", obj));
                return;
            case 2:
                HpUpdate HP = new HpUpdate(true, player);
                HP.ui(FindText("goodNew", obj));
                HP.badRefrash(FindText("badNew", obj));
                HP.Btn(FindButton("buy", obj));
                return;
            case 3:
                WudiUpdate WD = new WudiUpdate(true,player);
                WD.ui(FindText("goodNew", obj));
                WD.badRefrash(FindText("badNew", obj));
                WD.Btn(FindButton("buy", obj));
                return;
            case 4:
                ShotSpUpdate SS = new ShotSpUpdate(true,player);
                SS.ui(FindText("goodNew", obj));
                SS.badRefrash(FindText("badNew", obj));
                SS.Btn(FindButton("buy", obj));
                return;
            case 5:
                AtkCountUpdate AC = new AtkCountUpdate(true, player);
                AC.ui(FindText("goodNew", obj));
                AC.badRefrash(FindText("badNew", obj));
                AC.Btn(FindButton("buy", obj));
                return;
            case 6:
                AtkDisUpdate AD = new AtkDisUpdate(true, player);
                AD.ui(FindText("goodNew", obj));
                AD.badRefrash(FindText("badNew", obj));
                AD.Btn(FindButton("buy", obj));
                return;
            case 7:
                ExpUpdate EXP = new ExpUpdate(true,player);
                EXP.ui(FindText("goodNew", obj));
                EXP.badRefrash(FindText("badNew", obj));
                EXP.Btn(FindButton("buy", obj));
                return;
            case 8:
                MousSpUpdate MS = new MousSpUpdate(true,player);
                MS.ui(FindText("goodNew", obj));
                MS.badRefrash(FindText("badNew", obj));
                MS.Btn(FindButton("buy", obj));
                return;
            case 9:
                BoomUpdate BU = new BoomUpdate(true, player);
                BU.ui(FindText("goodNew", obj));
                BU.badRefrash(FindText("badNew", obj));
                BU.Btn(FindButton("buy", obj));
                return;
            default:
                return;
        }
    }
    //生成坏词条
    private void BadUI(GameObject obj)
    {
        int i = Random.Range(0, 10);
        switch (i)
        {
            case 0:
                AtkUpdate ATK = new AtkUpdate(false, player);
                ATK.ui(FindText("badNew", obj));
                ATK.Btn(FindButton("buy", obj));
                return;
            case 1:
                SpdUpdate SPD = new SpdUpdate(false, player);
                SPD.ui(FindText("badNew", obj));
                SPD.Btn(FindButton("buy", obj));
                return;
            case 2:
                HpUpdate HP = new HpUpdate(false, player);
                HP.ui(FindText("badNew", obj));
                HP.Btn(FindButton("buy", obj));
                return;
            case 3:
                WudiUpdate WD = new WudiUpdate(false, player);
                WD.ui(FindText("badNew", obj));
                WD.Btn(FindButton("buy", obj));
                return;
            case 4:
                ShotSpUpdate SS = new ShotSpUpdate(false, player);
                SS.ui(FindText("badNew", obj));
                SS.Btn(FindButton("buy", obj));
                return;
            case 5:
                AtkCountUpdate AC = new AtkCountUpdate(false, player);
                AC.ui(FindText("badNew", obj));
                AC.Btn(FindButton("buy", obj));
                return;
            case 6:
                AtkDisUpdate AD = new AtkDisUpdate(false, player);
                AD.ui(FindText("badNew", obj));
                AD.Btn(FindButton("buy", obj));
                return;
            case 7:
                ExpUpdate EXP = new ExpUpdate(false, player);
                EXP.ui(FindText("badNew", obj));
                EXP.Btn(FindButton("buy", obj));
                return;
            case 8:
                MousSpUpdate MS = new MousSpUpdate(false, player);
                MS.ui(FindText("badNew", obj));
                MS.Btn(FindButton("buy", obj));
                return;
            case 9:
                BoomUpdate BU = new BoomUpdate(false, player);
                BU.ui(FindText("badNew", obj));
                BU.Btn(FindButton("buy", obj));
                return;
            default:
                return;
        }
    }

    //关闭当前UI
    public void Close()
    { 
        gameObject.SetActive(false);
        btn1.missReverse();
        btn2.missReverse();
        btn3.missReverse();
    }

    //开启当前UI
    public void OpenUp()
    {
        gameObject.SetActive(true);
    }
}
