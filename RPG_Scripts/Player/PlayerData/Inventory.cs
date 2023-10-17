using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item
{
    #region 변수
    //재료, 물약 아이템
    private int _inventory_size = 32;
    private Item[] items = new Item[32];//파일로 넣어야 되는것들1
    public Item[] Inventory_Items => items;
    private List<int> inventory_empty_idx = new List<int>();//2
    public List<int> i_empty_list => inventory_empty_idx;
    private int _current_size = 0;//3
    public int _I_current_size => _current_size;
    private int _end_idx=0;//4
    public int _I_end_idx => _end_idx;
    //장비템
    private int _equipment_size = 32;
    private Item[] Equipment_items = new Item[32];
    public Item[] Equipment_Items => Equipment_items;
    private List<int> equipment_empty_idx = new List<int>();
    public List<int> e_empty_list => equipment_empty_idx;
    private int _e_current_size = 0;
    public int _E_current_size => _e_current_size;
    private int _e_end_idx = 0;
    public int _E_end_idx => _e_end_idx;
    //돈 아이템
    private int _money = 0;//
    public int Money => _money;
    
    #endregion

    #region 함수
    public bool AddItem(Item item, int amount=0)
    {
        if (item.GetType() == typeof(WeaponeItem)||item.GetType()==typeof(ArmorItem))
        {
            if (_e_current_size >= _equipment_size)
                return false;
            else
            {
                if (equipment_empty_idx.Count != 0)
                {
                    Equipment_items[equipment_empty_idx[0]] = item;
                    equipment_empty_idx.RemoveAt(0);
                }
                else
                {
                    Equipment_items[_e_end_idx] = item;
                    _e_end_idx++;
                }
                _e_current_size++;

                return true;
            }
        }
        else
        {
            for(int i = 0; i < _end_idx; i++)
            {
                if (items[i] != null && item.Data.ID==items[i].Data.ID)
                {
                    if (!(items[i] as CountableItem).IsMax)
                    {
                        (items[i] as CountableItem).AddAmountAndGetExcess(amount);
                        return true;
                    }
                }
            }

            if (_current_size >= _inventory_size)
                return false;
            else
            {
                if (inventory_empty_idx.Count != 0)
                {
                    items[inventory_empty_idx[0]] = item;
                    (items[inventory_empty_idx[0]] as CountableItem).SetAmount(amount);
                    inventory_empty_idx.RemoveAt(0);
                }
                else
                {
                    items[_end_idx] = item;
                    (items[_end_idx] as CountableItem).SetAmount(amount);
                    _end_idx++;
                }
                _current_size++;

                return true;
            }
        }
    }
    public void SetItemNumber(int idx,int number)
    {
        int rest = (items[idx] as CountableItem).AddAmountAndGetExcess(number);
        if ((items[idx] as CountableItem).IsEmpty)
        {
            DeleteItem(idx, true);
        }
    }
    //배열에 아이템 제거
    public void DeleteItem(int idx,bool isInventory)
    {
        if (isInventory)
        {
            items[idx] = null;
            _current_size--;
            i_empty_list.Add(idx);
            i_empty_list.Sort();

        }
        else
        {
            Equipment_items[idx] = null;
            _e_current_size--;
            e_empty_list.Add(idx);
            e_empty_list.Sort();
        }
    }
    //돈추가
    public void setMoney(int amount)
    {
        _money += amount;
    }
    public void Init(string _inven,string _eqiup,int _money)
    {
        ProcessItemStr(_inven);
        ProcessEquipStr(_eqiup);
        this._money = _money;
    }
    private void ProcessItemStr(string str)
    {
        int cnt = 0;
        int size = str.Length;//end_idx+1;
        for(int i = 1; i < size; i+=4)
        {
            string _id = str.Substring(i, 2);
            string _num = str.Substring(i + 2, 2);
            int.TryParse(_id, out int ID);
            int.TryParse(_num, out int num);
            if (ID == 0)
            {
                inventory_empty_idx.Add(cnt);
            }
            else if (DataManager.Instance.DictItem.ContainsKey(ID))
            {
                DataManager.Instance.DictItem.TryGetValue(ID, out ItemData data);
                CountableItem _item = data.CreateItem() as CountableItem;
                AddItem(_item, num);
                _current_size++;
            }
            cnt++;
        }
    }
    private void ProcessEquipStr(string str)
    {
        int cnt = 0;
        int size = str.Length;//end_idx+1;
        for (int i = 1; i < size; i += 4)
        {
            string _id = str.Substring(i, 2);
            int.TryParse(_id, out int ID);
            if (ID == 0)
            {
                equipment_empty_idx.Add(cnt);
            }
            else if (DataManager.Instance.DictItem.ContainsKey(ID))
            {
                DataManager.Instance.DictItem.TryGetValue(ID, out ItemData data);
                Item _item = data.CreateItem();
                AddItem(_item);
                _e_current_size++;
            }
            cnt++;
        }
    }
    public string SerializeInventory()
    {
        string str = "i";
        for(int i = 0; i < _end_idx; i++)
        {
            if (items[i] != null)
            {
                str += items[i].Data.ID.ToString();
                int num = (items[i] as CountableItem).Amount;
                if (num < 10)
                    str += "0";
                str += num.ToString();
            }
        }
        return str;
    }
    public string SerializeEquipment()
    {
        string str = "e";
        for (int i = 0; i < _e_end_idx; i++)
        {
            str += Equipment_items[i].Data.ID.ToString();
            str += "00";
        }
        return str;
    }
    #endregion
}
