using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : ButtonBase
{
    protected override void OnButtonClick()
    {
        Application.Quit();
    }
}
