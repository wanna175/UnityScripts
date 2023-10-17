using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    #region 변수
    public static Transform _infoPanel = null;
    [SerializeField] private Transform _slot_holder;
    [SerializeField] private Text _money = null;
    public Slot[] slots;
    public List<int> empty_idx;
    private int size = 32;
    public static int _select_idx = 0;
    private int _end_idx = 0;
    private int _current_size = 0;

    #endregion

    #region 함수
    private void Awake()
    {
        slots = new Slot[size];
        _end_idx = 0;
        _current_size = 0;
        _infoPanel = this.transform.GetChild(0);
        _infoPanel.gameObject.SetActive(false);
        for(int i = 0; i < size; i++)
        {
            slots[i] = _slot_holder.GetChild(i).GetComponent<Slot>();
            slots[i].Slot_idx = i;
        }
        //slots = _slot_holder.GetComponentsInChildren<Slot>();
        //size = slots.Length;

        //나중에 파일로 받아오게 하자...
    }
    private void Start()
    {
        SetInventoryUI();
    }
    
    //플레이어가 가지고 있는 아이템을 다시 넣어주는 과정..
    private void SetInventoryUI()
    {
        Item[] _item = PlayerManager.Instance._Items.Inventory_Items.Clone() as Item[];
        for (int i = 0; i < _item.Length; i++)
        {
            if(_item[i]!=null)
                slots[i].AddItem(_item[i],(_item[i] as CountableItem).Amount);
        }
        for (int i = 0; i < PlayerManager.Instance._Items.i_empty_list.Count; i++)
        {
            empty_idx.Add(PlayerManager.Instance._Items.i_empty_list[i]);
        }
        _current_size = PlayerManager.Instance._Items._I_current_size;
        _end_idx = PlayerManager.Instance._Items._I_end_idx;

        _money.text = PlayerManager.Instance._Items.Money.ToString();
    }

    //사용하기 버튼을 눌럿을 경우
    public void OnUseBtnClick()
    {
        int rest = slots[_select_idx].UseItem();
        PlayerManager.Instance._Items.SetItemNumber(_select_idx, -1);
        if (rest == 0)
        {
            OnDeleteBtnClick();
        }
    }
    //제거하기 버튼을 눌렀을 경우
    public void OnDeleteBtnClick()
    {
        slots[_select_idx].DeleteItem();
        _infoPanel.gameObject.SetActive(false);
        PlayerManager.Instance._Items.DeleteItem(_select_idx,true);
    }
    public bool AddInventoryItem(Item item)
    {
        for (int i = 0; i < _end_idx; i++)
            {
                if (slots[i].get_item != null && item.Data.ID == slots[i].get_item.Data.ID)
                {
                    if (!slots[i].IsFull)
                    {
                        slots[i].AddItem(item, (PlayerManager.Instance._Items.Inventory_Items[i] as CountableItem).Amount);
                        return true;
                    }
                }
            }

        if (_current_size >= size)
            return false;
        else
        {
            if (empty_idx.Count != 0)
            {
                slots[empty_idx[0]].AddItem(item, (PlayerManager.Instance._Items.Inventory_Items[empty_idx[0]] as CountableItem).Amount);
                empty_idx.RemoveAt(0);
            }
            else
            {
                slots[_end_idx].AddItem(item, (PlayerManager.Instance._Items.Inventory_Items[_end_idx] as CountableItem).Amount);
                _end_idx++;
            }
            _current_size++;

            return true;
        }
    }
    public void setMoney()
    {
        _money.text = PlayerManager.Instance._Items.Money.ToString();
    }
    #endregion

}
