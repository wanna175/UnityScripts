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
    //구매하기 버튼을 눌럿을 경우
    public void OnbuyBtnClick()
    {
        //나중에 기회가 되면 수량을 입력받자...
        if (slots[_select_idx].Data.Price * 10 > PlayerManager.Instance._Items.Money)
        {
            Debug.Log("돈이 부족합니다.");
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
            Debug.Log("인벤토리가 부족합니다.");
    }
    public void OnExitBtn()
    {
        this.gameObject.SetActive(false);
    }
}
