using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    #region ����
    public string Level => _level;
    [SerializeField] private string _level = null;
    #endregion
}
