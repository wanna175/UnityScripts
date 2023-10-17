using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUI : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI = null;
    [SerializeField] private EquipmentUI equipmentUI = null;
    [SerializeField] private market_slot[] slots;
    public static int _select_idx = 0;
    private int size = 2;
    private void Awake()
    {
        size = slots.Length;
        for (int i = 0; i < size; i++)
        {
            slots[i].Slot_idx = i;
        }
    }
    private void Start()
    {
        slots[0].Item_info();
    }
    //�����ϱ� ��ư�� ������ ���
    public void OnbuyBtnClick()
    {
        //���߿� ��ȸ�� �Ǹ� ������ �Է¹���...
        if (slots[_select_idx].Data.Price * 10 > PlayerManager.Instance._Items.Money)
        {
            Debug.Log("���� �����մϴ�.");
            return;
        }
        Item item = slots[_select_idx].Data.CreateItem();
        bool isAdd = PlayerManager.Instance._Items.AddItem(item, 1);
        if (isAdd)
        {
            inventoryUI.AddInventoryItem(item);
            PlayerManager.Instance._Items.setMoney(-slots[_select_idx].Data.Price * 10);
            inventoryUI.setMoney();
            equipmentUI.setMoney();
        }
        else
            Debug.Log("�κ��丮�� �����մϴ�.");
    }
    public void OnExitBtn()
    {
        this.gameObject.SetActive(false);
    }
}
