using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager05 : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.SCENE_NAME_05;
    #endregion

    #region 변수
    [Header("========던전 클리어 관련")]
    [SerializeField] private GameObject _clear_field = null;
    [SerializeField] private GameObject _clear_chest = null;
    private Animator _chestAni = null; 

    [Header("========던전 스테이지 관련")]
    [SerializeField] private MonsterSetting _monsterSetting = null;
    [SerializeField] private MonsterSetting _monsterSetting_02 = null;
    [SerializeField] private GameObject nextStage = null;
    [SerializeField] private List<GameObject> stage_doors = null;

    [Header("========플레이어 관련")]
    [SerializeField] private Transform _parent_player = null;
    private GameObject Player_clone=null;
    public static Vector3 _player_position = Vector3.zero;

    [Header("========플레이어 UI관련")]
    [SerializeField] private InventoryUIManager _invenUI = null;
    [SerializeField] private StatusUI _statUI = null;
    [SerializeField] private InformationPanel _infoPanel = null;
    [SerializeField] private GameObject _deathUI = null;
    #endregion

    #region 함수
    public override void Awake()
    {
        base.Awake();
        PlayerManager.Instance.SceneNum = 5;
        PlayerManager.Instance.current_floor = 0;
        PlayerManager.Instance.map_num = 0;
        Player_clone = Instantiate(PlayerManager.Instance._Player, _parent_player);
        Player_clone.transform.GetChild(0).transform.localPosition += Vector3.right * -2200;//시작 지점 설정하기..
        Player_clone.transform.GetChild(1).transform.localPosition += Vector3.right * -2200;
    }
    public override void Start()
    {
        base.Start();
        _chestAni = _clear_chest.transform.GetChild(1).gameObject.GetComponent<Animator>();
    }
    public override void Update()
    {
        base.Update();
        _player_position = Player_clone.transform.GetChild(1).localPosition;//더 좋은 방법이 없을까..
        if (isMoveMap_num)
        {
            SetPlayerPos();
        }
        if (_monsterSetting._monster_count == 0)
        {
            StageClear();
        }
        if (_monsterSetting_02._monster_count == 0&&_monsterSetting._monster_count==-1)
        {
            finalStageClear();
        }
        if (PlayerManager.Instance.IsDeath)
        {
            _deathUI.SetActive(true);
            PlayerManager.Instance.IsDeath = false;
        }
        OpenUI();
        clickChest();
    }
    private void SetPlayerPos()
    {
        int num = PlayerManager.Instance.map_num;
        Player_clone.transform.GetChild(0).transform.localPosition += Vector3.right * num*4400 + Vector3.up * num * 1500;//시작 지점 설정하기..
        Player_clone.transform.GetChild(1).transform.localPosition += Vector3.right * num*4400 + Vector3.up * num * 1500;
        foreach (var door in stage_doors)
            door.gameObject.SetActive(false);
        isMoveMap_num = false;
    }
    private void StageClear()
    {
        nextStage.SetActive(true);
        _monsterSetting_02.gameObject.SetActive(true);
        foreach (var door in stage_doors)
            door.gameObject.SetActive(true);
        _monsterSetting._monster_count = -1;
    }
    private void finalStageClear()
    {
        Debug.Log("던전을 클리어 하셨습니다.");
        _monsterSetting_02._monster_count = -1;
        _clear_field.SetActive(true);
        _clear_chest.SetActive(true);
    }
    private void OpenUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _invenUI.gameObject.SetActive(true);
            _invenUI.transform.SetAsLastSibling();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _statUI.gameObject.SetActive(true);
            _statUI.transform.SetAsLastSibling();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _invenUI.OnExitBtn();
            _statUI.OnExitBtn();
        }
    }
    private void clickChest()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Chest"))
                {
                    _chestAni.SetBool("isClick", true);
                    _infoPanel.AlertInfo("5000", "money");
                    PlayerManager.Instance._Items.setMoney(5000);
                }
            }
        }
    }
    public void OnYesbtn()
    {
        LoadingSceneManager.LoadScene(Global.SCENE_NAME_04);
    }
    #endregion
}
