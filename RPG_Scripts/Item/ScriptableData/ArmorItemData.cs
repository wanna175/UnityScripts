using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Armor", menuName = "Item Data/Armor")]
public class ArmorItemData : EquipmentItemData
{
    #region 변수
    public int Def => _def; 
    [SerializeField] private int _def;
    #endregion

    #region 함수
    public override Item CreateItem()
    {
        return new ArmorItem(this);
    }
    #endregion
}
