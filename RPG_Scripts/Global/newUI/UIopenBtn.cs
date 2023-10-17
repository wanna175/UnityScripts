using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIopenBtn : MonoBehaviour, IPointerClickHandler
{
    #region º¯¼ö
    [SerializeField] protected GameObject _UI = null;
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        _UI.SetActive(true);
        _UI.GetComponent<RectTransform>().SetAsLastSibling();
    }
}
