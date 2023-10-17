using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //(싱글톤 시작할때 )게임을 시작할때 플레이어 관련 데이터를 읽어오자=>
    //캐릭터 선택 창에서 파일을 읽어오는 스크립트 하나 작성후 거기서 파일을 읽어오자...
    #region 싱글톤 관련
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
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

    #region 변수 (플레이어가 가지고 있어야 하는 데이터들)
    private GameObject _player = null;//캐릭터 프리팹
    private Player_Item _items = null;//인벤토리
    private Status _status = null;//캐릭터 스텟
    
    public int _PlayerNumber { get;  set; }//세이브할때 어떤 리스트번호에 저장을 할지 인덱스
    public Status _Status => _status;
    public Player_Item _Items => _items;
    public GameObject _Player => _player;
    public int isSkill { get; set; }
    public int map_num { get; set; }//이동하는 층-1,1...
    public int current_floor { get; set; }////현재 씬에서 위치한 맵 넘버
    public bool IsDeath = false;
    
    public int SceneNum { get; set; }
    #endregion

    #region 함수
    private void Awake()
    {
        SceneNum = -1;
        current_floor = 0;
        map_num = 0;
        isSkill = -1;
        InitSingleton();
    }
    public void SetPlayer(GameObject player,Player_Item inventory, Status status,int number)
    {
        _player = player;
        _items = inventory;
        _status = status;
        _PlayerNumber = number;
    }
    public Player_data GetData()
    {
        return new Player_data(_player, _items, _status);
    }
    public int GetIDX()
    {
        return _PlayerNumber;
    }
    #endregion
}
