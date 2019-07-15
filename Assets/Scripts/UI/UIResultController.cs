using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultController : UIControllerBase
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("txt_score").GetComponent<Text>();   
    }

    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.RESULT)
            SetResult();
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    private void SetResult()
    {
        scoreText.text = PlayManager.Instance.Score.ToString();
        canvas.enabled = true;
    }

    public void RePlay()
    {
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.STAGE);
    }
}
