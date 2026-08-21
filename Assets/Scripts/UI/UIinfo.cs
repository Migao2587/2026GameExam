using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIinfo : MonoBehaviour
{
    public TMP_Text kill;
    public TMP_Text alive;
    public TMP_Text score;
    public long killcount = 0;
    public long scorecount = 0;
    private float livetime = 0;
    public ExpManager EXP;

    private void Awake()
    {
        GameEvent.OnMousKill += reFresh;
        TMP_Text[] allText = GetComponentsInChildren<TMP_Text>(true);
        foreach (var text in allText)
            {
                switch (text.tag)
                {
                    case "killCount":
                            kill = text;
                        continue;
                    case "aliveTime":
                        alive = text;
                        continue;
                    case "score":
                        score = text;
                        continue;
                    default:
                        continue;
                }
            }
    }
    private void Update()
    {
        livetime += Time.deltaTime;
        TIME(livetime);
    }

    //更新显示
    public void reFresh()
    {
        killcount++;
        scorecount = (long)(livetime / 100 * killcount * EXP.currentLevel);
        kill.text = ""+ killcount;
        score.text = "" + scorecount;
    }
    //时间转换
    private void TIME(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int sec = (int)(time % 60);
        alive.text = $"{min:D2}:{sec:F1}";
    }

    //存档
    public UISaveData SaveData()
    {
        return new UISaveData()
        {
            killcount = this.killcount,
            scorecount = this.scorecount,
            livetime = this.livetime,
        };
    }
    //读档
    public void LoadData(UISaveData data)
    { 
        this.killcount = data.killcount;
        this.scorecount = data.scorecount;
        this.livetime = data.livetime;
        reFresh();
    }
}
