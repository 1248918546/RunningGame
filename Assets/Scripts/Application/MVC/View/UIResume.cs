using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{
    public Image imgCount;
    public Sprite[] spCount;

    public override string Name => Consts.V_Resume;

    public override void HandleEvent(string name, object data)
    {

    }

    public void StartCount()
    {
        Show();
        StartCoroutine(StartCountCoroutine());
    }

    IEnumerator StartCountCoroutine()
    {
        int i = 3;
        while(i > 0)
        {
            imgCount.sprite = spCount[i - 1];
            i--;
            yield return new WaitForSeconds(1);
            if (i <= 0) 
                break;
        }
        Hide();

        //放在Controller中做
        //GameModel gm = GetModel<GameModel>();
        //gm.IsPause = false;
        //gm.IsPlay = true; 
        SendEvent(Consts.E_ContinueGame, SendMessageOptions.RequireReceiver);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
