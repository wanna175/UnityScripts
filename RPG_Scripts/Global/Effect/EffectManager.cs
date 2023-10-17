using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region 싱글톤 관련
    private static EffectManager _instance = null;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("EffectManager").AddComponent<EffectManager>();
                _instance = obj;
            }
            return _instance;
        }
    }
    public static EffectManager Create()
    {
        if (_instance != null)
        {
            return null;
        }
        return EffectManager.Instance;
    }
    private void InitSingleton()
    {
        Debug.Assert(_instance == null);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private Queue<Effect> pooling_queue = new Queue<Effect>();//아이템 풀링
    private GameObject _eff_prefab = null;

    private void Awake()
    {
        InitSingleton();
        _eff_prefab = Resources.Load<GameObject>("Prefabs/Effect/Effect");
        Initalize_pool(10);
    }

    #region 오브젝트 풀링 관련
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //새로운 객체를 생성한다.
    private Effect Create_object()
    {
        var obj = Instantiate(_eff_prefab, this.transform).GetComponent<Effect>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //큐에 존재하는 객체를 가져온다.
    public Effect Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_object(Effect obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
#endregion
}
