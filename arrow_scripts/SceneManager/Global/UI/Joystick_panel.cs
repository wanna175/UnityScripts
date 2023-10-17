using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick_panel : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{
    #region 변수,프로퍼티
    [SerializeField] private RectTransform _joystick = null;
    [SerializeField] private RectTransform _stick = null;

    private float _horizontal = 0;
    private float _vertical = 0;
    public float Horizontal
    {
        get
        {
            return _horizontal;
        }
    }
    public float Vertical
    {
        get
        {
            return _vertical;
        }
    }
    #endregion

    #region 함수
    private void Awake()
    {
        _joystick.gameObject.SetActive(false);
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        _joystick.anchoredPosition = eventData.position;
        _joystick.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _stick.anchoredPosition = eventData.position - _joystick.anchoredPosition;
        _horizontal = _stick.anchoredPosition.x;
        _vertical = _stick.anchoredPosition.y;
        if (_stick.anchoredPosition.magnitude > 180.0f){
            _stick.anchoredPosition = _stick.anchoredPosition.normalized * 180.0f;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _horizontal = 0;
        _vertical = 0;
        _joystick.gameObject.SetActive(false);
    }
    #endregion
}
