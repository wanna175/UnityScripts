using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    #region �̱��� ����
    private static DamageManager _instance = null;
    public static DamageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("DamageManager").AddComponent<DamageManager>();
                _instance = obj;
            }
            return _instance;
        }
    }
    public static DamageManager Create()
    {
        if (_instance != null)
        {
            return null;
        }
        return DamageManager.Instance;
    }
    private void InitSingleton()
    {
        Debug.Assert(_instance == null);
        DontDestroyOnLoad(this.gameObject);
    }
#endregion
    #region ����
    private GameObject _Damagetxt_prefab = null;
    private Queue<Damage_txt> pooling_queue = new Queue<Damage_txt>();//������ Ǯ��
    public Sprite[] _damage_sprites = null; 
    #endregion

    #region �Լ�
    private void Awake()
    {
        InitSingleton();
        _Damagetxt_prefab = Resources.Load<GameObject>("Prefabs/Effect/damage_Effect");
        _damage_sprites = Resources.LoadAll<Sprite>("Sprite/Damage/damage_Effect");
        Initalize_pool(10);
    }
    public Damage_txt Get_Damage_Effect(int value,bool isCritical,bool isPlayer = false)
    {
        var obj = Get_Object();
        obj.SetDamage(value,isCritical,isPlayer);
        return obj;
    }
    #endregion



    #region ������Ʈ Ǯ�� ����
    //�ʱⰪ���� ť�� �̸� 10��ŭ �����س���...
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //���ο� ��ü�� �����Ѵ�.
    private Damage_txt Create_object()
    {
        var obj = Instantiate(_Damagetxt_prefab, this.transform).GetComponent<Damage_txt>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //ť�� �����ϴ� ��ü�� �����´�.
    private Damage_txt Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_object(Damage_txt obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
