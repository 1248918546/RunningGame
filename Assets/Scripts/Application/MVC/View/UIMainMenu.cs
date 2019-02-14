using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    public override string Name => Consts.V_MainMenu;

    public override void HandleEvent(string name, object data)
    {

    }

    public void OnPlayClick()
    {
        Game.Instance.LoadLevel(4);
    }
}
