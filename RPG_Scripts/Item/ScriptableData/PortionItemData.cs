using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion", menuName = "Item Data/Portion")]
public class PortionItemData : CountableItemData
{
    #region 변수
    public int Hp => _hp;
    public int Mp => _mp;

    [SerializeField] private int _hp;
    [SerializeField] private int _mp;
    #endregion

    #region 함수
    //아이템을 생성한다.
    public override Item CreateItem()
    {
        return new PortionItem(this) as Item;
    }
    #endregion
}
