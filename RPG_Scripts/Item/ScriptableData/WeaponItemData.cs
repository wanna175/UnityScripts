using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Weapon", menuName = "Item Data/Weapon")]
public class WeaponItemData : EquipmentItemData
{
    #region 변수
    public int Off => _off;
    [SerializeField] private int _off;
    #endregion

    #region 함수
    public override Item CreateItem()
    {
        return new WeaponeItem(this);
    }
    #endregion
}
