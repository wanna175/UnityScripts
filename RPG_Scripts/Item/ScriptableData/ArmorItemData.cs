using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Armor", menuName = "Item Data/Armor")]
public class ArmorItemData : EquipmentItemData
{
    #region ����
    public int Def => _def; 
    [SerializeField] private int _def;
    #endregion

    #region �Լ�
    public override Item CreateItem()
    {
        return new ArmorItem(this);
    }
    #endregion
}
