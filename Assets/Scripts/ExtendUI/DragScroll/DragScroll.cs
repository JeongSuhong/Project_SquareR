using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScroll : ScrollRect, IPointerDownHandler
{

    private IDragScrollItem dragItem;
    private Vector2 startPointerPos;
    private bool isDragItem;
    private bool isScrolling;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragItem = false;
        isScrolling = false;
        startPointerPos = eventData.position;
        dragItem = null;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isScrolling)
        {
            if (Mathf.Abs(startPointerPos.y - eventData.position.y) > 10)       // Drag Skill
            {
                IDragScrollItem item = eventData.pointerPressRaycast.gameObject.GetComponent<IDragScrollItem>();

                if (item != null && dragItem != item)
                {
                    dragItem = item;
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
