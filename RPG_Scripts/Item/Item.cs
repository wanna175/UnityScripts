using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData Data { get; private set; }
    public Item(ItemData data) => Data = data;

}
public abstract class CountableItem : Item
{
    public CountableItemData CountableData { get; private set; }
    public int Amount { get; protected set; }//현재 갯수
    public int MaxAmount => CountableData.MaxAmount;//최대로 가질 수 있는 갯수
    public bool IsMax => Amount >= CountableData.MaxAmount;//수량이 가득 찼는지 여부
    public bool IsEmpty => Amount <= 0; //갯수가 없는지 여부
    public CountableItem(CountableItemData data, int amount = 0) : base(data)
    {
        CountableData = data;
        SetAmount(amount);
    }
    //개수범위를 제한한다.
    public void SetAmount(int amount)
    {
        Amount = Mathf.Clamp(amount, 0, MaxAmount);
    }
    //개수 추가 및 최대치 초과량 반환(초과량 없을 경우 0)
    public int AddAmountAndGetExcess(int amount)
    {
        int nextAmount = Amount + amount;
        SetAmount(nextAmount);

        return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
    }
}
public class PortionItem : CountableItem 
{
    public PortionItem(PortionItemData data, int amount = 0) : base(data, amount) { }

    public void UseItem()
    {
         ItemValue();
    }
    private void ItemValue()
    {
        PortionItemData _data = Data as PortionItemData;
        PlayerManager.Instance._Status.SetHealth(_data.Hp, false);
        //mp는 만들지 않았다...아직
    }
}

public abstract class EquipmentItem : Item
{
    public EquipmentItem(EquipmentItemData data) : base(data) { }
}

public class WeaponeItem : EquipmentItem
{
    public WeaponeItem(WeaponItemData data) : base(data) { }
}

public class ArmorItem : EquipmentItem
{
    public ArmorItem(ArmorItemData data) : base(data) { }
}