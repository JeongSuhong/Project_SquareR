using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerBase : MonoBehaviour
{
    protected Canvas canvas;

    protected virtual void Start()
    {
        canvas = GetComponent<Canvas>();
        UIManager.Instance.OnUpdateUI += UpdateUI;
    }

    protected virtual void UpdateUI(DataDefine.GAME_STATE state) { }
}
