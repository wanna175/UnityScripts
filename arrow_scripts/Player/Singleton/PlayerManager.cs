using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region �̱��� ����
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

    #region ����
    public int select_idx;
    private List<GameObject> _player_list = new List<GameObject>();
    private GameObject _current_player;
    #endregion
    #region �Լ�
    //�ʱ�ȭ
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
    //���� ���õ��÷��̾��ȯ�Ѵ�.
    public GameObject GetPlayer()
    {
        return _current_player;
    }
    //�÷��̾ �����Ѵ�.
    public void SelectPlayer(int idx)
    {
        _current_player = _player_list[idx];
    }
    #endregion
}
