using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectSceneManager : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.G_SCENE_NAME_01;
    #endregion

    #region 함수
    //초기화
    public override void Awake()
    {
        base.Awake();
    }
    //상태를 갱신한다.
    public override void Update()
    {
        base.Update();
    }
    //시작버튼이 눌렸을 경우
    public void On_select_01_button() {
        LoadingSceneManager.LoadScene(Global.G_SCENE_NAME_02);
        PlayerManager.Instance.SelectPlayer(0);
    }
    public void On_select_02_button()
    {
        LoadingSceneManager.LoadScene(Global.G_SCENE_NAME_02);
        PlayerManager.Instance.SelectPlayer(1);
    }
    #endregion

}
