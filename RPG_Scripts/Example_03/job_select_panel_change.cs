using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class job_select_panel_change : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _change_panel = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        _change_panel.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }
 
}
