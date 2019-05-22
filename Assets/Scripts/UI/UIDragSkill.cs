using System;
using UnityEngine;
using UnityEngine.UI;

public class UIDragSkill : MonoBehaviour
{
    public DataDefine.SKILL_TYPE CurrentType { get; private set; }
    private Image iconImage;
    private RectTransform rect;

    private Vector2 movePos;

    private bool isInit;

    public delegate void OnDragDelegate(Vector2 movePos);
    public OnDragDelegate OnDrag;
    public delegate void OnEndDragDelegate();
    public OnEndDragDelegate OnEndDrag;

    private void Init()
    {
        iconImage = GetComponentInChildren<Image>();
        rect = transform as RectTransform;
    }

    public void View(DataDefine.SKILL_TYPE type, Sprite icon)
    {
        if (!isInit)
            Init();

        CurrentType = type;
        iconImage.sprite = icon;
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
        OnDrag?.Invoke(transform.position);

        if (Input.GetMouseButtonUp(0))
            OnDragEnd();
    }

    private void OnDragEnd()
    {
        OnEndDrag?.Invoke();
        OnEndDrag = null;
        OnDrag = null;

        CurrentType = DataDefine.SKILL_TYPE.TEMP;

        gameObject.SetActive(false);
    }
}
