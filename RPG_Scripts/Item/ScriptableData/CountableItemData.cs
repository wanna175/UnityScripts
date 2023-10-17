using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItemData : ItemData
{
    #region 변수
    public int MaxAmount => _maxAmount;
    [SerializeField] private int _maxAmount = 99;//가질수 있는 최대 용량
    #endregion
}
