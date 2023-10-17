using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerClickHandler
{
    #region º¯¼ö
    [SerializeField] private GameObject _Menu_panel;
    [SerializeField] private Transform _Menu_panel_Root;
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0.0f;
        Instantiate(_Menu_panel, _Menu_panel_Root);
    }

}
