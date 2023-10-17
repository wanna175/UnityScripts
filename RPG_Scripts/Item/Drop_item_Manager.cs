using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_item_Manager : MonoBehaviour
{
    #region ����
    [SerializeField] private Transform _floor = null;
    [SerializeField] private GameObject _drop_itme_prefab = null;
    private Queue<Drop_Item> pooling_queue = new Queue<Drop_Item>();//������ Ǯ��
    //������ ������ ����Ʈ..���߿� �ʿ� ������ ���Ͱ� ��ȯ�ϴ� �����͸� �޾ƿ���..������ �ӽ�
    [SerializeField] private List<ItemData> _itemData_list = null;

    [SerializeField] private GameObject inventoryUI = null;
    public GameObject invenUI => inventoryUI;
    [SerializeField] private GameObject equipmentUI = null;
    public GameObject equipUI => equipmentUI;
    [SerializeField] private InformationPanel _infoPanel = null;
    public InformationPanel InfoPanel => _infoPanel;
    #endregion

    #region �Լ�
    private void Awake()
    {
        Initalize_pool(10);
    }
    //�������� �ʱ�ȭ ���� �Ѱ��ش�.
    private Drop_Item Get_Item_object()
    {
        int Rand = Random.Range(0, _itemData_list.Count);
        if (Rand >= _itemData_list.Count)
        {
            Rand = 0;
        }
        var item = Get_Object();
        item.SetItem(_itemData_list[Rand]);

        return item;
    }
    private Drop_Item Get_Item_object(int _monster_id)
    {
        //���߿� ��ȸ�� �Ǹ� id�� ���� �ٸ��� �̾ƺ���...
        int Rand = Random.Range(0, _itemData_list.Count);
        if (Rand >= _itemData_list.Count)
        {
            Rand = 0;
        }
        var item = Get_Object();
        item.SetItem(_itemData_list[Rand]);

        return item;
    }
    //�ӽ÷� �������� �Ѹ���.
    public void Drop_item_toField_temp()
    {
        var item1 = Get_Item_object();
        item1.transform.position = new Vector3(400, _floor.position.y + 20, 0);
        item1.gameObject.SetActive(true);
        var item2 = Get_Item_object();
        item2.transform.position = new Vector3(700, _floor.position.y + 20, 0);
        item2.gameObject.SetActive(true);
        var item3 = Get_Item_object();
        item3.transform.position = new Vector3(1000, _floor.position.y + 20, 0);
        item3.gameObject.SetActive(true);
        var item4 = Get_Item_object();
        item4.transform.position = new Vector3(100, _floor.position.y + 20, 0);
        item4.gameObject.SetActive(true);
        var item5 = Get_Item_object();
        item5.transform.position = new Vector3(-200, _floor.position.y + 20, 0);
        item5.gameObject.SetActive(true);
        var item6 = Get_Item_object();
        item6.transform.position = new Vector3(-500, _floor.position.y + 20, 0);
        item6.gameObject.SetActive(true);
        var item7 = Get_Item_object();
        item7.transform.position = new Vector3(-800, _floor.position.y + 20, 0);
        item7.gameObject.SetActive(true);
        var item8 = Get_Item_object();
        item8.transform.position = new Vector3(-1100, _floor.position.y + 20, 0);
        item8.gameObject.SetActive(true);
        var item9 = Get_Item_object();
        item9.transform.position = new Vector3(-1400, _floor.position.y + 20, 0);
        item9.gameObject.SetActive(true);
    }

    //���Ͱ� �׾����� �������� ������.
    public void Drop_item_toField(Vector3 _monster_pos,int _monster_num)
    {
        var item = Get_Item_object();
        item.transform.position = _monster_pos + Vector3.up *-100f;
        item.gameObject.SetActive(true);
        
    }
    #endregion



    #region ������Ʈ Ǯ�� ����
    //�ʱⰪ���� ť�� �̸� 10��ŭ �����س���...
    private void Initalize_pool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //���ο� ��ü�� �����Ѵ�.
    private Drop_Item Create_object()
    {
        var obj = Instantiate(_drop_itme_prefab, this.transform).GetComponent<Drop_Item>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //ť�� �����ϴ� ��ü�� �����´�.
    public Drop_Item Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_object(Drop_Item obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
