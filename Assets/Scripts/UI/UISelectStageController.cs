using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectStageController : UIControllerBase
{
    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.SELECT)
            canvas.enabled = true;
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    public void OnClickStage()
    {
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.STAGE);
    }

    public void OnMoveMain()
    {
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.START);
    }
}
