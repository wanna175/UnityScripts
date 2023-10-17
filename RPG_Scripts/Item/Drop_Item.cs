using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Item : MonoBehaviour
{
    //아이템을 생성하는 쪽에서 초기화를 해주자...몬스터가 죽을 때라던지..보물상자가 열릴때던지...풀매니져를 만들어야 겟다.
    #region 변수
    private ItemData _itemdata = null;//드랍된 아이템 정보
    private SpriteRenderer _spriteRenderer = null;//떨궈진 아이템 스프라이트

    private Drop_item_Manager _item_manager = null;
    private InventoryUI inventoryUI = null;
    private EquipmentUI equipmentUI = null;
    #endregion

    #region 함수
    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _item_manager = this.gameObject.GetComponentInParent<Drop_item_Manager>();
        inventoryUI = _item_manager.invenUI.GetComponent<InventoryUI>();
        equipmentUI = _item_manager.equipUI.GetComponent<EquipmentUI>();
    }
    //아이템을 초기화 한다.
    public void SetItem(ItemData data)
    {
        _itemdata = data;
        _spriteRenderer.sprite = data.IconSprite;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        Item item = _itemdata.CreateItem();
        bool isAdd = PlayerManager.Instance._Items.AddItem(item,1);
        
        if (isAdd)
        {
            _item_manager.InfoPanel.AlertInfo(item.Data.Name,"item");
            if (item.GetType() == typeof(WeaponeItem) || item.GetType() == typeof(ArmorItem))
            {
                equipmentUI.AddEqipmentItem(item);
            }
            else
            {
                inventoryUI.AddInventoryItem(item);
            }
            _item_manager.Return_object(this);
        }
    }
    #endregion
}
