using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : ButtonBase
{
    protected override void OnButtonClick()
    {
        ChangeScene("MainScene");
    }
}
