using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //(�̱��� �����Ҷ� )������ �����Ҷ� �÷��̾� ���� �����͸� �о����=>
    //ĳ���� ���� â���� ������ �о���� ��ũ��Ʈ �ϳ� �ۼ��� �ű⼭ ������ �о����...
    #region �̱��� ����
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

    #region ���� (�÷��̾ ������ �־�� �ϴ� �����͵�)
    private GameObject _player = null;//ĳ���� ������
    private Player_Item _items = null;//�κ��丮
    private Status _status = null;//ĳ���� ����
    
    public int _PlayerNumber { get;  set; }//���̺��Ҷ� � ����Ʈ��ȣ�� ������ ���� �ε���
    public Status _Status => _status;
    public Player_Item _Items => _items;
    public GameObject _Player => _player;
    public int isSkill { get; set; }
    public int map_num { get; set; }//�̵��ϴ� ��-1,1...
    public int current_floor { get; set; }////���� ������ ��ġ�� �� �ѹ�
    public bool IsDeath = false;
    
    public int SceneNum { get; set; }
    #endregion

    #region �Լ�
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
