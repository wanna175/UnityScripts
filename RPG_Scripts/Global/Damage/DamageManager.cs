using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    #region 싱글톤 관련
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
    #region 변수
    private GameObject _Damagetxt_prefab = null;
    private Queue<Damage_txt> pooling_queue = new Queue<Damage_txt>();//아이템 풀링
    public Sprite[] _damage_sprites = null; 
    #endregion

    #region 함수
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



    #region 오브젝트 풀링 관련
    //초기값으로 큐에 미리 10만큼 생성해놓자...
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //새로운 객체를 생성한다.
    private Damage_txt Create_object()
    {
        var obj = Instantiate(_Damagetxt_prefab, this.transform).GetComponent<Damage_txt>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //큐에 존재하는 객체를 가져온다.
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
