using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    #region 변수
    [SerializeField] private RectTransform _Drag_UI = null;//드래그 할 창
    private Vector2 downPos;
    #endregion
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 _offset = eventData.position - downPos;
        downPos = eventData.position;

        _Drag_UI.anchoredPosition += _offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        downPos = eventData.position;
        _Drag_UI.SetAsLastSibling();
    }
}
