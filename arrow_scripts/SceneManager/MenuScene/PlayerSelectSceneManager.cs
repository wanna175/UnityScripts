using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectSceneManager : SceneManager_parent
{
    #region ������Ƽ
    public override string SceneName => Global.G_SCENE_NAME_01;
    #endregion

    #region �Լ�
    //�ʱ�ȭ
    public override void Awake()
    {
        base.Awake();
    }
    //���¸� �����Ѵ�.
    public override void Update()
    {
        base.Update();
    }
    //���۹�ư�� ������ ���
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
