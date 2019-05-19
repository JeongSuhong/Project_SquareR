

using System;
using UnityEngine;

public interface IDragScrollItem 
{
  /// <summary>
  /// View Drag Item
  /// </summary>
  /// <param name="viewItemAction">Icon & DragEndAction</param>
    void OnStartDrag(Action<Sprite, Action> viewItemAction);
}
