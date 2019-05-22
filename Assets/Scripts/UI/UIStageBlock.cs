using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageBlock : MonoBehaviour
{
    public DataDefine.BLOCK_TYPE BlockType; //{ get; private set; }
    private DataDefine.SKILL_TYPE skillType;

    private Image background;
    private Image itemIcon;

    private bool isChangedSprite;
    private bool isInit;

    public void SetBlockData(DataDefine.BLOCK_TYPE block, DataDefine.SKILL_TYPE skill = DataDefine.SKILL_TYPE.TEMP)
    {
        if (!isInit)
            InitBlock();

        BlockType = block;
        skillType = skill;

        SetBlockSprite(block, skillType);
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

    private void InitBlock()
    {
        background = GetComponent<Image>();
        itemIcon = GetComponentsInChildren<Image>()[1];
        itemIcon.gameObject.SetActive(false);

        isInit = true;
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
                SetBlockSprite(BlockType, skillType);
            }
        }
        else
        {
            if (targetBlockList.Contains(this))
                SetBlockData(DataDefine.BLOCK_TYPE.SKILL, type);
        }
    }
}
