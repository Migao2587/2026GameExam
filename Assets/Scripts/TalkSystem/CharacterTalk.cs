using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTalk : MonoBehaviour
{
    //立绘
    public Sprite charSprite;
    //名字
    public string charName;
    //文本
    [TextArea] public string charText;

    //打包发送对话
    public void SentTalk()
    {
        var data = new TalkBase()
        {
            charname = charName,
            charSprite = charSprite,
            content = charText
        };

    }

}
