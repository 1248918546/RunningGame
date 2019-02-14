using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Text txtDistance;
    public Text txtCoin;
    public Text txtScore;

    public override string Name => Consts.V_Pause;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ShowScore(int distance, int coin, int score)
    {
        txtDistance.text = distance.ToString();
        txtCoin.text = coin.ToString();
        txtScore.text = score.ToString();
    }

    public override void HandleEvent(string name, object data)
    {

    }

    public void OnResumeClick()
    {
        Hide();
        SendEvent(Consts.E_ResumeGame);
    }
}
