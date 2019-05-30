using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageSkill : MonoBehaviour, IDragScrollItem
{
    public DataDefine.SKILL_TYPE Type { get; private set; }

    public int Count { get; private set; }
    public int OriginCount { get; private set; }
    public Image SkillBG { get; private set; }

    private Image countBG;
    private Text countText;

    private Action<UIStageSkill> startSkillAction;

    private void Awake()
    {
        Image[] childImages = GetComponentsInChildren<Image>();
        SkillBG = childImages[0];
        countBG = childImages[1];
        countText = GetComponentInChildren<Text>();
    }

    public void SetSkill(DataDefine.SKILL_TYPE type, int count, Action<UIStageSkill> startAction)
    {
        Type = type;
        OriginCount = Count = count;
        startSkillAction = startAction;

        SkillBG.sprite = UIManager.Instance.AtlasManager.SkillSprites[(int)type];
        countBG.sprite = UIManager.Instance.AtlasManager.SkillCountSprites[(int)type];
        countBG.gameObject.SetActive(true);
        SetCount(count);
    }

    public void SetEmptySkill()
    {
        SkillBG.sprite = UIManager.Instance.AtlasManager.EmptyBlockSprite;
        countBG.gameObject.SetActive(false);
    }

    public void SetCount(int addCount)
    {
        Count += addCount;
        countText.text = Count.ToString();
    }

    public void OnStartDrag()
    {
        if (Count > 0)
        {
            SetCount(-1);
            startSkillAction?.Invoke(this);
        }
    }
}
