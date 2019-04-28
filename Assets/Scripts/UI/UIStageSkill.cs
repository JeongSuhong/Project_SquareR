using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageSkill : MonoBehaviour
{
    public DataDefine.SKILL_TYPE Type { get; private set; }

    public int Count { get; private set; }
    public int OriginCount { get; private set; }

    private Image skillBG;
    private Image countBG;
    private Text countText;

    private void Awake()
    {
        Image[] childImages = GetComponentsInChildren<Image>();
        skillBG = childImages[0];
        countBG = childImages[1];
        countText = GetComponentInChildren<Text>();
    }

    public void SetSkill(DataDefine.SKILL_TYPE type, int count)
    {
        Type = type;
        OriginCount = Count = count;
        
        skillBG.sprite = UIManager.Instance.AtlasManager.SkillSprites[(int)type];
        countBG.sprite = UIManager.Instance.AtlasManager.SkillCountSprites[(int)type];
        countText.text = Count.ToString();
        countBG.gameObject.SetActive(true);
    }

    public void SetEmptySkill()
    {
        skillBG.sprite = UIManager.Instance.AtlasManager.EmptyBlockSprite;
        countBG.gameObject.SetActive(false);
    }
}
