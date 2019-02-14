using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    public Text txtDistance;
    public Text txtCoin;
    public Text txtGoal;
    public Text txtScore;

    public Slider sliExp;
    public Text txtExp;
    public Text txtGrade;

    public override string Name => Consts.V_FinalScore;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    //更新UI
    public void UpdateUI(int distance, int coin, int goal, int exp, int grade)
    {
        //1.距离
        txtDistance.text = distance.ToString();
        //2.金币
        txtCoin.text = coin.ToString();
        //3.分数
        txtScore.text = (distance * (goal + 1) + coin).ToString();
        //4.进球
        txtGoal.text = goal.ToString();
        //5.exp slider text
        txtExp.text = exp.ToString() + "/" + (10 + grade * 10).ToString();
        //6.exp slider value
        sliExp.value = (float)exp / (10 + grade * 10);
        //7.grade
        txtGrade.text = grade.ToString() + "级";
    }

    public override void HandleEvent(string name, object data)
    {
    }

    public void OnReplayClick()
    {
        Game.Instance.LoadLevel(4);
    }
}
