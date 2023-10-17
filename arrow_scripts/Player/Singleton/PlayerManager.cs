using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region 싱글톤 관련
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    { get{
            if (_instance == null)
            {
                var obj = new GameObject("PlayerManager").AddComponent<PlayerManager>();
                _instance = obj;
            }
            return _instance;
        } 
    }
    public static PlayerManager Create()
    {
        if (_instance != null)
            return null;
        return PlayerManager.Instance;
    }
    private void InitSingleton()
    {
        Debug.Assert(_instance == null);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region 변수
    public int select_idx;
    private List<GameObject> _player_list = new List<GameObject>();
    private GameObject _current_player;
    #endregion
    #region 함수
    //초기화
    private void Awake()
    {
        InitSingleton();
        foreach (var item in Resources.LoadAll<GameObject>("Player/Prefab"))
            _player_list.Add(item);
        _current_player = _player_list[0];
    }
    private void Start(){
    }
    private void Update()
    {
        
    }
    //현재 선택된플레이어반환한다.
    public GameObject GetPlayer()
    {
        return _current_player;
    }
    //플레이어를 선택한다.
    public void SelectPlayer(int idx)
    {
        _current_player = _player_list[idx];
    }
    #endregion
}
