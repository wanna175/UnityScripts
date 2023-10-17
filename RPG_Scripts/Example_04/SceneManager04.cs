using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager04 : SceneManager_parent
{
    #region ������Ƽ
    public override string SceneName => Global.SCENE_NAME_04;
    #endregion

    #region ����
    [SerializeField] private Transform _parent_player = null;
    private GameObject Player_clone=null;

    [SerializeField] private InventoryUIManager _invenUI = null;
    [SerializeField] private StatusUI _statUI = null;
    #endregion

    #region ������ ���� ����
    [SerializeField] private Drop_item_Manager _item_manager = null;
    #endregion

    #region �Լ�
    public override void Awake()
    {
        base.Awake();
        if (PlayerManager.Instance.SceneNum == 5) {//�������� ������ ���� ���
            PlayerManager.Instance.current_floor = -1;
            PlayerManager.Instance.map_num = 0;
            PlayerManager.Instance._Status.SetHealth((int)PlayerManager.Instance._Status.HP, false);
            Player_clone = Instantiate(PlayerManager.Instance._Player, _parent_player);
            Player_clone.transform.GetChild(0).transform.localPosition += Vector3.up*-1500+Vector3.right * 2000; //���� ���� �����ϱ�..
            Player_clone.transform.GetChild(1).transform.localPosition += Vector3.up*-1500+Vector3.right * 2000;
        }
        else {
            PlayerManager.Instance.current_floor = 0;
            PlayerManager.Instance.map_num = 0;
            Player_clone = Instantiate(PlayerManager.Instance._Player, _parent_player);
            Player_clone.transform.GetChild(0).transform.localPosition += Vector3.right * -2200; //���� ���� �����ϱ�..
            Player_clone.transform.GetChild(1).transform.localPosition += Vector3.right * -2200;
        }
    }
    public override void Start()
    {
        base.Start();
        _item_manager.Drop_item_toField_temp();
    }
    public override void Update()
    {
        base.Update();
        if (isMoveMap_num)
        {
            SetPlayerPos();
        }
        OpenUI();
        clickNPC();
    }
    private void SetPlayerPos()
    {
        int num = PlayerManager.Instance.map_num;
        Player_clone.transform.GetChild(0).transform.localPosition += Vector3.right * num*4400 + Vector3.up * num * 1500;//���� ���� �����ϱ�..
        Player_clone.transform.GetChild(1).transform.localPosition += Vector3.right * num*4400 + Vector3.up * num * 1500;
        isMoveMap_num = false;
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
    private void clickNPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("���콺 Ŭ���޽�0");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.CompareTag("Npc"))
                {
                    hit.transform.gameObject.GetComponent<Npc>().ClickNpc();
                }
            }
        }
    }
    #endregion
}
