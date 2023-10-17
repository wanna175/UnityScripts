using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject _current_tab = null;
    #endregion
    #region 함수
    private void Awake()
    {
        InventoryTab._current_tab = _current_tab;
    }
    public void OnExitBtn()
    {

        this.gameObject.SetActive(false);

    }
    #endregion
}
