using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TalkBase
{
    public Sprite charSprite;

    public string charname;

    [TextArea] public string content;

    public Action Start;

    public Action End;
}
