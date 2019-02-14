using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ReuseableObject
{
    public Animation goalKeeper;
    public Animation door;
    public GameObject net;

    public float speed = 10;
    bool IsFly = false;

    public override void OnSpawn()
    {

    }

    public override void OnUnSpawn()
    {
        goalKeeper.Play("standard");
        door.Play("QiuMen_St");
        net.SetActive(true);
        goalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(true);
        goalKeeper.transform.localPosition = Vector3.zero;
        IsFly = false;
        StopAllCoroutines();
    }

    //进球
    public void ShootAGoal(int xpos)
    {
        switch (xpos)
        {
            case -2:
                goalKeeper.Play("left_flutter");
                break;
            case 0:
                goalKeeper.Play("flutter");
                break;
            case 2:
                goalKeeper.Play("right_flutter");
                break;
        }
        StartCoroutine(HideGoalKeeperCoroutine());
    }

    IEnumerator HideGoalKeeperCoroutine()
    {
        yield return new WaitForSeconds(1);
        goalKeeper.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }

    //撞飞守门员
    public void HitGoalKeeper()
    {
        IsFly = true;
        goalKeeper.Play("fly");
    }

    //撞到球门
    void HitBallDoor(int index)
    {
        //1.球门动画
        switch(index)
        {
            case 0:
                door.Play("QiuMen_RR");
                break;
            case 1:
                door.Play("QiuMen_St");
                break;
            case 2:
                door.Play("QiuMen_LR");
                break;
        }
        //2.球网消失
        net.SetActive(false);
    }


    private void Update()
    {
        if(IsFly)
        {
            goalKeeper.transform.position += new Vector3(0, speed * Time.deltaTime, speed * Time.deltaTime);
        }
    }
}
