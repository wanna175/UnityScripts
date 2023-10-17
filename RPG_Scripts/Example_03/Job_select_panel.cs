using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job_select_panel : MonoBehaviour
{
    #region 변수
    [SerializeField] GameObject _player_prefab = null;
    private Animator _ani = null;
    [SerializeField] private int _num;

    [SerializeField] private GameObject _characters_panel = null;
    #endregion

    #region 함수
    public void Awake()
    {
        _ani = this.transform.GetComponentInChildren<Animator>();
    }
    public void OnSelectbtn()
    {
        Player_data _data = new Player_data(_player_prefab,new Player_Item(),new Status());
        DataManager.Instance._list.Add(_data);
        DataManager.Instance.SaveData();
        this.gameObject.transform.parent.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        SceneManager_03.isChange = true;

        _characters_panel.SetActive(true);
    }
    public void OnBackbtn()
    {
        this.gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(false);
        SceneManager_03.isChange = true;
        _characters_panel.SetActive(true);
    }
    private void OnEnable()
    {
        _ani.SetInteger("select", _num);
        _characters_panel.SetActive(false);
    }
  
    #endregion
}
