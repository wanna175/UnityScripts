using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_03 : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.PLAYER_SELECT_SCENE;
    #endregion

    #region 변수
    [SerializeField] private GameObject _job_select_panel = null;
    [SerializeField] private GameObject _info_panel = null;
    [SerializeField] private List<GameObject> _select_character = null;
    private int current_char_num;

    private Color _color;
    public static bool isChange = false;
    #endregion

    #region 함수
    public override void Awake()
    {
        base.Awake();
        current_char_num = -1;
        _color = _select_character[0].GetComponent<Image>().color;
        DataManager.Create();
        
    }
    public override void Start()
    {
        base.Start();
        ReadPlayerData();
        ShowCharacter();
    }
    public override void Update()
    {
        base.Update();
        if (isChange)
            ShowCharacter();
    }
    private void ReadPlayerData()
    {
        DataManager.Instance.LoadData();
        isChange = true;
    }
    public void OnPlayBtn()
    {
        PlayerManager.Create();
        PlayerManager.Instance.SetPlayer(DataManager.Instance._list[current_char_num]._player
            ,DataManager.Instance._list[current_char_num]._item,DataManager.Instance._list[current_char_num]._status,current_char_num);
        EffectManager.Create();
        DamageManager.Create();
        LoadingSceneManager.LoadScene(Global.SCENE_NAME_04);
    }
    private void OnPlayerBtn(int num)
    {
        num -= 1;
        if (DataManager.Instance._list.Count <= num)
        {
            Debug.Log("캐릭터가 없습니다. 생성해 주세요");
            _info_panel.SetActive(false);
            return;
        }
        _info_panel.SetActive(true);
        _info_panel.transform.GetChild(0).GetComponent<Text>().text = $"레벨 : {DataManager.Instance._list[num]._status.LV}";
        _info_panel.transform.GetChild(1).GetComponent<Text>().text = $"직업 : {DataManager.Instance._list[num]._player.name.Substring(7)}";
        current_char_num = num;
    }
    public void OnPlayerBtn1()
    {
        OnPlayerBtn(1);
    }
    public void OnPlayerBtn2()
    {
        OnPlayerBtn(2);
    }
    public void OnPlayerBtn3()
    {
        OnPlayerBtn(3);
    }
    public void OnPlayerBtn4()
    {
        OnPlayerBtn(4);
    }
    
    public void CreateCharacter()
    {
        if (DataManager.Instance._list.Count >= 4)
        {
            Debug.Log("캐릭터창이 다 찼습니다.");
            return;
        }
        _info_panel.SetActive(false);
        _job_select_panel.SetActive(true);
        _job_select_panel.transform.GetChild(1).gameObject.SetActive(false);
        _job_select_panel.transform.GetChild(2).gameObject.SetActive(false);
        _job_select_panel.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DeleteCharacter()
    {
        DataManager.Instance._list.RemoveAt(current_char_num);
        DataManager.Instance.SaveData();
        _info_panel.SetActive(false);
        isChange = true;
    }
    public void Exit()
    {

    }
    public void ShowCharacter()
    {
        int count = DataManager.Instance._list.Count;
        for(int i = 0; i < count; i++)
        {
            var showcase = _select_character[i].GetComponentInChildren<Animator>();
            switch (DataManager.Instance._list[i]._player.name.Substring(7))
            {
                case "Knight":
                    showcase.SetInteger("character", 0);
                    break;
                case "Archer":
                    showcase.SetInteger("character", 1);
                    break;
                case "Wizard":
                    showcase.SetInteger("character", 2);
                    break;
            }
            _select_character[i].GetComponent<Image>().color = new Color(_color.r, _color.g, _color.b, 1);
            showcase.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 1);
        }
        for (int i = count; i < 4; i++)
        {
            var showcase = _select_character[i].GetComponentInChildren<Animator>();
            _select_character[i].GetComponent<Image>().color = new Color(_color.r, _color.g, _color.b, 0.35f);
            showcase.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 0);
        }
        isChange = false;
    }
 
    #endregion
}
