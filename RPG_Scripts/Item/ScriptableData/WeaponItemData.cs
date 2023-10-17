using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Weapon", menuName = "Item Data/Weapon")]
public class WeaponItemData : EquipmentItemData
{
    #region ����
    public int Off => _off;
    [SerializeField] private int _off;
    #endregion

    #region �Լ�
    public override Item CreateItem()
    {
        return new WeaponeItem(this);
    }
    #endregion
}
