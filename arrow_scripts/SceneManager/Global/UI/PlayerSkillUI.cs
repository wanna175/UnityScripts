using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillUI : MonoBehaviour, IPointerClickHandler
{
    #region ����
    public static bool isClick = false;
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = true;
    }
}
