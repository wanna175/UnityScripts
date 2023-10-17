using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIopenBtn : UIopenBtn
{

    #region ÇÔ¼ö
    private void Start()
    {
        var arr = _UI.GetComponentsInChildren<enableClick>();
        foreach(var idx in arr)
        {
            idx.gameObject.SetActive(false);
        }
        arr[0].gameObject.SetActive(true);
        _UI.SetActive(false);
        
    }
    #endregion
}
