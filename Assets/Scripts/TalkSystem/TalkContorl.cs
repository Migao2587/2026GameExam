using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkContorl : MonoBehaviour
{
    //是否处于对话
    private bool isPlaying;
    //对话框预制体
    public GameObject Dialog;

    private bool outAble;
    //全局单例
    public static TalkContorl Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        { 
            Instance = this;
        }
    }

    [Header("对话框预制体")]

    //对话队列
    private Queue<TalkBase> contQueue = new Queue<TalkBase>();
    //当前对话
    public TalkBase talkNow;


    //外部对话加入队列
    public void EnterQueue(TalkBase data)
    { 
        contQueue.Enqueue(data);
        if (!isPlaying)
        { 
            playerNext();
        }
    }

    //取出对话并播放
    private void playerNext()
    {
        if (contQueue.Count == 0)
        {
            isPlaying = false;
            talkNow = null;
            Time.timeScale = 1f;
            PlayerInput2.Instance.WASD(true);
            PlayerInput2.Instance.ESC(true);
            return;

        }
        isPlaying = true;
        talkNow = contQueue.Dequeue();
        
        Debug.Log(contQueue.Count);

        //实例化对话栏
        createTalk(talkNow);
    }

    //实例化对话框
    private void createTalk(TalkBase cont)
    {
        talkNow = cont;
        cont.End += talkEnd;
        GameObject dialog = Instantiate(Dialog.gameObject,gameObject.transform);
        DialogBase dia = dialog.GetComponent<DialogBase>();
        dia.setSelf(cont);
        
    }
    //轮播函数解绑
    private void talkEnd()
    {
        if (talkNow != null)
        { 
            talkNow.End -= talkEnd;
        }
        playerNext();
    }
    
}
