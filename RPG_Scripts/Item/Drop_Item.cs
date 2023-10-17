using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Item : MonoBehaviour
{
    //�������� �����ϴ� �ʿ��� �ʱ�ȭ�� ������...���Ͱ� ���� �������..�������ڰ� ����������...Ǯ�Ŵ����� ������ �ٴ�.
    #region ����
    private ItemData _itemdata = null;//����� ������ ����
    private SpriteRenderer _spriteRenderer = null;//������ ������ ��������Ʈ

    private Drop_item_Manager _item_manager = null;
    private InventoryUI inventoryUI = null;
    private EquipmentUI equipmentUI = null;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _item_manager = this.gameObject.GetComponentInParent<Drop_item_Manager>();
        inventoryUI = _item_manager.invenUI.GetComponent<InventoryUI>();
        equipmentUI = _item_manager.equipUI.GetComponent<EquipmentUI>();
    }
    //�������� �ʱ�ȭ �Ѵ�.
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
