using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBase : MonoBehaviour
{
    //缓存对话信息
    public Image spriteImg;
    public TMP_Text charname;
    public TMP_Text content;

    private TalkBase curData;
    private Coroutine typeCor;

    private bool over = false;

    //初始化
    private void Awake()
    {
        Transform name = transform.Find("name");
        GameObject nameTemp = name.gameObject;
        charname = nameTemp.GetComponent<TMP_Text>();

        Transform cont = transform.Find("content");
        GameObject contTemp = cont.gameObject;
        content = contTemp.GetComponent<TMP_Text>();

        Transform image = transform.Find("image");
        GameObject imageTemp = image.gameObject;
        spriteImg = imageTemp.GetComponent<Image>();

        Time.timeScale = 0;
        PlayerInput2.Instance.WASD(false);
        PlayerInput2.Instance.ESC(false);
    }
    //载入数据
    public void setSelf(TalkBase data)
    { 
        curData = data;
        data.Start?.Invoke();

        spriteImg.sprite = data.charSprite;
        charname.text = data.charname;
        content.text = "";

        if (typeCor != null)
        { 
            StopCoroutine(typeCor);
        }
        typeCor = StartCoroutine(TypeAnim(curData.content));
    }
    //点击跳过
    public void SkipCor()
    {
        if (content.text == curData.content || over)
        {
            curData.End?.Invoke();
            Destroy(gameObject);

        }
        if (typeCor != null)
        {
            StopCoroutine(typeCor);
            content.text = curData.content;
        }
    }
    //文字显示
    IEnumerator TypeAnim(string text)
    {
        foreach (var c in text)
        { 
            content.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        over = true;
    }

}
