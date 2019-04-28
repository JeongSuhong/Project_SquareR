using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageController : UIControllerBase
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject skillPrefab;

    private UIStageBlock[,] blockObjects;
    private RectTransform mapGridRect;

    private UIStageSkill[] skillObjects;
    private RectTransform skillGridRect;

    protected override void Start()
    {
        base.Start();

        mapGridRect = transform.Find("Group_Map").GetComponentInChildren<GridLayoutGroup>().GetComponent<RectTransform>();
        skillGridRect = transform.Find("Group_Skills").GetComponentInChildren<HorizontalLayoutGroup>().GetComponent<RectTransform>();
    }

    protected override void UpdateUI(DataDefine.GAME_STATE state)
    {
        if (state == DataDefine.GAME_STATE.STAGE)
        {
            SetBlocksUI();
            SetSkillsUI();
            canvas.enabled = true;
        }
        else if (canvas.enabled)
            canvas.enabled = false;
    }

    private void SetBlocksUI()
    {
        DataDefine.BLOCK_TYPE[,] map = StageManager.Instance.MapData;

        int count = map.GetLength(0);

        if (mapGridRect.childCount == 0)
            blockObjects = InitMapBlocks(count);

        for (int x = 0; x < count; x++)
        {
            for (int y = 0; y < count; y++)
                blockObjects[x, y].SetBlockData(map[x, y]);
        }
    }

    private UIStageBlock[,] InitMapBlocks(int count)
    {
        UIStageBlock[,] result = new UIStageBlock[count, count];
        string blockObjectName = "block_{0}_{1}";

        for (int x = 0; x < count; x++)
        {
            for (int y = 0; y < count; y++)
            {
                GameObject block = Instantiate(blockPrefab, mapGridRect);
                block.name = string.Format(blockObjectName, x, y);
                result[x, y] = block.GetComponent<UIStageBlock>();
            }
        }

        return result;
    }

    private void SetSkillsUI()
    {
        Dictionary<DataDefine.SKILL_TYPE, int> skill = StageManager.Instance.SkillData;
        int skillCount = skill.Count;
        int index = 0;

        if (skill.Count < 4)
            skillCount = 4;

        skillObjects = InitSkills(skillCount);
        
        foreach(DataDefine.SKILL_TYPE skillType in skill.Keys)
        {
            skillObjects[index].SetSkill(skillType, skill[skillType]);
            index++;
        }

        for (int i = index; i < skillObjects.Length; i++)
            skillObjects[i].SetEmptySkill();
    }

    private UIStageSkill[] InitSkills(int count)
    {
        UIStageSkill[] result = new UIStageSkill[count];
        string skillObjectName = "skill_{0}";
        int createCount = count - skillGridRect.childCount;

        for (int i = 0; i < createCount; i++)
        {
            GameObject skill = Instantiate(skillPrefab, skillGridRect);
            skill.name = string.Format(skillObjectName, i);
            result[i] = skill.GetComponent<UIStageSkill>();
        }

        return result;
    }
}
