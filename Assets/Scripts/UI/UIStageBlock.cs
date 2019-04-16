using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageBlock : MonoBehaviour
{
    public DataDefine.BLOCK_TYPE Type; //{ get; private set; }

    private Image icon;
    private bool init;

    public void SetBlockData(DataDefine.BLOCK_TYPE type)
    {
        if (!init)
            InitBlock();

        Type = type;

        switch (Type)
        {
            case DataDefine.BLOCK_TYPE.TARGET:
                icon.sprite = UIManager.Instance.AtlasManager.TargetSprite;
                break;
        }
    }

    private void InitBlock()
    {
        icon = GetComponentInChildren<Image>();

        init = true;
    }
}
