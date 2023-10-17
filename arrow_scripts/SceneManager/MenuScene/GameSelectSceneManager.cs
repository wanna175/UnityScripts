using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSelectSceneManager : SceneManager_parent
{
    #region 프로퍼티
    public override string SceneName => Global.G_SCENE_NAME_00;
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
    public void On_select_01_button()
    {
        //게임에 따라 리스트든 뭐든 받아서 보내자
        SceneManager.LoadScene(Global.G_SCENE_NAME_01);
        ResultStorage.Instance.SelectGame(0);
    }
    #endregion

}
