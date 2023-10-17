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
    public int Amount { get; protected set; }//���� ����
    public int MaxAmount => CountableData.MaxAmount;//�ִ�� ���� �� �ִ� ����
    public bool IsMax => Amount >= CountableData.MaxAmount;//������ ���� á���� ����
    public bool IsEmpty => Amount <= 0; //������ ������ ����
    public CountableItem(CountableItemData data, int amount = 0) : base(data)
    {
        CountableData = data;
        SetAmount(amount);
    }
    //���������� �����Ѵ�.
    public void SetAmount(int amount)
    {
        Amount = Mathf.Clamp(amount, 0, MaxAmount);
    }
    //���� �߰� �� �ִ�ġ �ʰ��� ��ȯ(�ʰ��� ���� ��� 0)
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
        //mp�� ������ �ʾҴ�...����
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