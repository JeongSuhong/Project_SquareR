using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScroll : ScrollRect, IPointerDownHandler
{
    private Vector2 startPointerPos;
    private bool isDragItem;
    private bool isScrolling;

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

                if (dragItem != null)
                {
                    dragItem.OnStartDrag();
                    isDragItem = true;
                }
            }
        }

        if (!isDragItem)
        {
            isScrolling = true;
            base.OnDrag(eventData);
        }
    }
}
