using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventoryTab : MonoBehaviour,IPointerClickHandler
{
    #region º¯¼ö
    public static GameObject _current_tab = null;
    [SerializeField] private GameObject _tabed_UI = null;
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_current_tab.name.Equals(_tabed_UI.name))
        {
            return;
        }
        _current_tab.SetActive(false);
        _current_tab = _tabed_UI;
        _tabed_UI.SetActive(true);
    }
}
