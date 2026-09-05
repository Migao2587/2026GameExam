using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class charaSend : MonoBehaviour
{
    public Sprite normal;
    public Sprite haixiu;

    private void Awake()
    {
        StartTeach();
    }

    public void StartTeach()
    {
        TalkBase t01 = new TalkBase()
        {
            charname = "主角儿",
            content = "欢迎来到“随机大冒险”。名字是随便乱取的，但至少这个名字还是很扣题的。",
            charSprite = normal,
        };
        TalkBase t02 = new TalkBase()
        {
            charname = "主角儿",
            content = "WASD控制方向。ESC呼出暂停菜单。当然，当进入某些场景时，这些按键可能会被禁用。",
            charSprite = normal,
        };
        TalkBase t03 = new TalkBase()
        {
            charname = "主角儿",
            content = "在这个有限地图中，怪物会持续刷新，你需要合理操作活下去。祝好运！（怪物寻路机制很呆，别在意，因为我就只给了方向速度去寻路喔）",
            charSprite = haixiu,
        };
        TalkContorl.Instance.EnterQueue(t01);
        TalkContorl.Instance.EnterQueue(t02);
        TalkContorl.Instance.EnterQueue(t03);
    }
}
