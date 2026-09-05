using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    TalkBase testTalk = new TalkBase();
    private void Start()
    {
        testTalk.charname = "米糕";
        testTalk.content = "这是一个测试文本。这是一个测试文本。这是一个测试文本。这是一个测试文本。这是一个测试文本。这是一个测试文本。";
    }

    public void StartTest()
    {
        TalkContorl.Instance.EnterQueue(testTalk);
        TalkContorl.Instance.EnterQueue(testTalk);
    }
}
