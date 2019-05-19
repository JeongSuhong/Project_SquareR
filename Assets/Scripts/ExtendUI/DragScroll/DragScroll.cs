using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScroll : ScrollRect, IPointerDownHandler
{
    private IViewDragItem viewDragItem;

    private Vector2 startPointerPos;
    private bool isDragItem;
    private bool isScrolling;

    protected override void Awake()
    {
        base.Awake();
        viewDragItem = transform.parent.GetComponentInChildren<IViewDragItem>(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragItem = false;
        isScrolling = false;
        startPointerPos = eventData.position;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isScrolling)
        {
            if (Mathf.Abs(startPointerPos.y - eventData.position.y) > 10)       // Drag Skill
            {
                IDragScrollItem dragItem = eventData.pointerPressRaycast.gameObject.GetComponent<IDragScrollItem>();
                dragItem.OnStartDrag(viewDragItem.View);
                isDragItem = true;
            }
        }

        if (!isDragItem)
        {
            isScrolling = true;
            base.OnDrag(eventData);
        }
    }
}
