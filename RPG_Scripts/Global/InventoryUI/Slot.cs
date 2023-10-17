using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerClickHandler
{
    #region ����
    public Item get_item => _item;
    private Item _item = null;
    private Image _item_image = null;
    private int amount = 0;

    public bool IsFull=>isFull;
    private bool isFull = false;

    public int Slot_idx { get; set; }

    private Text _item_name_txt = null;
    private Text _item_grade_txt = null;
    private Text _item_info_txt = null;

    [SerializeField] private Transform _infoPanel = null;
    [SerializeField] private InventoryUI _inventoryUI = null;
    [SerializeField] private EquipmentUI _equipmentUI = null;
    private Text _item_count_txt = null;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _item_image = this.gameObject.GetComponent<Image>();
        if (_infoPanel.name.Equals("inventory_Panel"))
        {
            _item_count_txt = this.transform.GetChild(0).GetComponent<Text>();
            _item_count_txt.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        _item_name_txt = _infoPanel.GetChild(0).GetComponent<Text>();
        _item_grade_txt = _infoPanel.GetChild(1).GetComponent<Text>();
        _item_info_txt = _infoPanel.GetChild(2).GetComponent<Text>();
    }
    //���Կ� �������� �����Ѵ�.
    public void AddItem(Item item)
    {
        Debug.Log(item.Data.name);
        if (item == null)
            return;
        isFull = true;
        _item = item;
        _item_image.sprite = item.Data.IconSprite;
    }
    public void AddItem(Item item, int amount)//�������� �����ϴ� ������ ����
    {
        if (item == null)
            return;
        _item = item;
        this.amount = amount;
        _item_image.sprite = item.Data.IconSprite;
        if ((_item as CountableItem).MaxAmount <= amount)
        {
            isFull = true;
            this.amount = (_item as CountableItem).MaxAmount;
        }
        _item_count_txt.text = $"{this.amount}";
        _item_count_txt.gameObject.SetActive(true);
        
    }
    //���Կ� �������� �����Ѵ�.
    public int DeleteItem()
    {
        isFull = false;
        _item = null;
        _item_image.sprite = null;

        if (_infoPanel.name.Equals("inventory_Panel")){
            _inventoryUI.empty_idx.Add(Slot_idx);
            _inventoryUI.empty_idx.Sort();    
            _item_count_txt.text = "0";
            _item_count_txt.gameObject.SetActive(false);
        }
        else
        {
            _equipmentUI.empty_idx.Add(Slot_idx);
            _equipmentUI.empty_idx.Sort();
        }
        return Slot_idx;
    }
    //������ Ŭ���� ������ inventoryUI�� �����Ѵ�.
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_infoPanel.name.Equals("inventory_Panel"))
            InventoryUI._select_idx = Slot_idx;
        else
            EquipmentUI._select_idx = Slot_idx;
        if (_item != null)
            Item_info();
        else
            _infoPanel.gameObject.SetActive(false);
 
    }
    public int UseItem()
    {
        Debug.Log("�������� ����Ͽ����ϴ�.");
        (_item as PortionItem).UseItem();
        amount--;

        _item_count_txt.text = $"{amount}";
        return amount;
    }
    private void Item_info()
    {
        _item_name_txt.text = _item.Data.Name;
        _item_grade_txt.text = _item.Data.Grade;
        _item_info_txt.text = _item.Data.Tooltip;
        _infoPanel.gameObject.SetActive(true);
        if (_item.GetType() != typeof(PortionItem))
        {
            _infoPanel.GetChild(3).GetChild(0).gameObject.SetActive(false);
        }
    }
    #endregion
}
