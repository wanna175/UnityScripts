using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_merchant : Npc
{
    #region ����
    [SerializeField] private GameObject _marketUI = null;
    #endregion
    public override void ClickNpc()
    {
        _marketUI.SetActive(true);
    }
}
