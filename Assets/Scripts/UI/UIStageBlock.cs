using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageBlock : MonoBehaviour
{
    public int Index { get; private set; }
    private Image background;
    private Image itemIcon;

    private bool isChangedSprite;
    private bool isInit;

    private void InitBlock()
    {
        background = GetComponent<Image>();
        itemIcon = GetComponentsInChildren<Image>()[1];
        itemIcon.gameObject.SetActive(false);

        isInit = true;
    }

    public void SetBlock(int index, BlockData data)
    {
        if (!isInit)
            InitBlock();

        Index = index;
    
        SetBlockSprite(data.BlockType, data.SkillType);
    }

    private void SetBlockSprite(DataDefine.BLOCK_TYPE type, DataDefine.SKILL_TYPE skill)
    {
        switch (type)
        {
            case DataDefine.BLOCK_TYPE.TARGET:
                background.sprite = UIManager.Instance.AtlasManager.TargetBlockSprite;
                break;
            case DataDefine.BLOCK_TYPE.TRAP:
                background.sprite = UIManager.Instance.AtlasManager.TrapBlockSprite;
                break;
            case DataDefine.BLOCK_TYPE.SKILL:
                background.sprite = UIManager.Instance.AtlasManager.SkillSprites[(int)skill];
                break;
            default:
                background.sprite = UIManager.Instance.AtlasManager.EmptyBlockSprite;
                break;
        }
    }

    public void UpdateBlock(DataDefine.SKILL_TYPE type, List<UIStageBlock> targetBlockList, bool isPreview)
    {
        if (isPreview)
        {
            if (targetBlockList.Contains(this))
            {
                SetBlockSprite(DataDefine.BLOCK_TYPE.SKILL, type);
                isChangedSprite = true;
            }
            else if (isChangedSprite)
            {
                isChangedSprite = false;
                BlockData originData = PlayManager.Instance.GetBlockData(Index);
                SetBlockSprite(originData.BlockType, originData.SkillType);
            }
        }
    }
}
