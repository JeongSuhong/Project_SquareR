using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageBlock : MonoBehaviour
{
    public DataDefine.BLOCK_TYPE Type; //{ get; private set; }

    private Image background;
    private Image itemIcon;
    private bool init;

    public void SetBlockData(DataDefine.BLOCK_TYPE type)
    {
        if (!init)
            InitBlock();

        Type = type;

        switch (Type)
        {
            case DataDefine.BLOCK_TYPE.TARGET:
                background.sprite = UIManager.Instance.AtlasManager.TargetBlockSprite;
                break;
            case DataDefine.BLOCK_TYPE.TRAP:
                background.sprite = UIManager.Instance.AtlasManager.TrapBlockSprite;
                break;
            default:
                background.sprite = UIManager.Instance.AtlasManager.EmptyBlockSprite;
                break;
        }
    }

    private void InitBlock()
    {
        background = GetComponent<Image>();
        itemIcon = GetComponentsInChildren<Image>()[1];
        itemIcon.gameObject.SetActive(false);

        init = true;
    }
}
