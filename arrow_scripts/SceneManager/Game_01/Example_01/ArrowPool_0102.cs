using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool_0102 : MonoBehaviour
{
    #region ����
    [SerializeField] private GameObject _arrow_prefab = null;
    private Queue<Arrow_0102> pooling_queue = new Queue<Arrow_0102>();
    #endregion

    #region �Լ�
    //�ʱ�ȭ
    private void Awake(){
        Initialize_pool(10);
    }
    //�ʱⰪ���� ť�� 10��ŭ �̸� �����س���=>�����̱�
    private void Initialize_pool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //���ο� ��ü�� �����Ѵ�.
    private Arrow_0102 Create_object()
    {
        var obj = Instantiate(_arrow_prefab,this.transform).GetComponent<Arrow_0102>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //ť�� �����ϴ� ��ü�� �����´�.
    public Arrow_0102 Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_Object(Arrow_0102 obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
