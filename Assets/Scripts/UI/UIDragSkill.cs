using System;
using UnityEngine;
using UnityEngine.UI;

public class UIDragSkill : MonoBehaviour, IViewDragItem
{
    private Image iconImage;
    private RectTransform rect;

    private Vector2 movePos;

    private Action onDragEndAction;

    private bool isInit;

    private void Init()
    {
        iconImage = GetComponentInChildren<Image>();
        rect = transform as RectTransform;
    }

    public void View(Sprite icon, Action dragEndAction)
    {
        if (!isInit)
            Init();

        iconImage.sprite = icon;
        onDragEndAction = dragEndAction;
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            OnDragEnd();
    }

    private void OnDragEnd()
    {
        onDragEndAction?.Invoke();
        onDragEndAction = null;
        gameObject.SetActive(false);
    }
}
