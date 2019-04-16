using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartController : UIControllerBase
{

    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.START)
            canvas.enabled = true;
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    public void MoveStartForButton()
    {
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.STAGE);
    }


}
