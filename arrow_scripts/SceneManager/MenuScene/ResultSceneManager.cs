using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : SceneManager_parent
{
    #region ����
    [SerializeField] private Text _current_scoreTxt = null;
    [SerializeField] private Text _high_scoreTxt = null;
    [SerializeField] private Text _current_coin = null;
    #endregion

    #region ������Ƽ
    public override string SceneName => Global.G_SCENE_NAME_Result;
    #endregion

    #region �Լ�
    public override void Awake()
    {
        _current_scoreTxt.text = $"{ResultStorage.Instance.Score:0.00}";
        _high_scoreTxt.text = $"{ResultStorage.Instance.gamescore_list[ResultStorage.Instance._current_game_number]:0.00}";
        _current_coin.text = $"{ResultStorage.Instance.Coin}";
    }
    //�ٽ��ϱ� ��ư�� ��������
    public void On_click_restart()
    {
        LoadingSceneManager.LoadScene(Global.G_SCENE_NAME_00);
    }
    #endregion
}
