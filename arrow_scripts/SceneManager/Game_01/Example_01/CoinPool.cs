using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    #region ����
    [SerializeField] private GameObject _coin_prefab = null;
    private Queue<Coin> pooling_queue = new Queue<Coin>();
    #endregion

    #region �Լ�
    /*�ʱ�ȭ*/
    private void Awake()
    {
        Initialize_pool(3);
    }
    //ť �ʱ�ȭ
    private void Initialize_pool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //���ο� ��ü�� �����Ѵ�.
    private Coin Create_object()
    {
        var obj = Instantiate(_coin_prefab, this.transform).GetComponent<Coin>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    public Coin Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_Object(Coin obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
