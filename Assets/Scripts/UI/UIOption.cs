using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOption : MonoBehaviour
{
    private int viewIndex;

    private void Awake()
    {
        viewIndex = 0;
    }

    public void View()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public void Close()
    {
        GetComponent<Canvas>().enabled = false;
    }

    private void ViewOption(int index)
    {
        transform.GetChild(index + 2).gameObject.SetActive(false);

        viewIndex = index;
        transform.GetChild(index + 2).gameObject.SetActive(true);
    }

    public void OnClickMoveHome()
    {
        Close();
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.SELECT);
    }

    public void OnClickRetry()
    {
        Close();
        WorldManager.Instance.ChangeGameState(DataDefine.GAME_STATE.STAGE);
    }

    public void OnClickViewStaff()
    {
        ViewOption(1);
    }

    public void OnClickContinue()
    {
        Close();
    }
}
