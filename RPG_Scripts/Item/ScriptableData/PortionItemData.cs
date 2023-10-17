using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion", menuName = "Item Data/Portion")]
public class PortionItemData : CountableItemData
{
    #region ����
    public int Hp => _hp;
    public int Mp => _mp;

    [SerializeField] private int _hp;
    [SerializeField] private int _mp;
    #endregion

    #region �Լ�
    //�������� �����Ѵ�.
    public override Item CreateItem()
    {
        return new PortionItem(this) as Item;
    }
    #endregion
}
