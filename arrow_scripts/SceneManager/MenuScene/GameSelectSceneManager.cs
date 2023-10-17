using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSelectSceneManager : SceneManager_parent
{
    #region ������Ƽ
    public override string SceneName => Global.G_SCENE_NAME_00;
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
    public void On_select_01_button()
    {
        //���ӿ� ���� ����Ʈ�� ���� �޾Ƽ� ������
        SceneManager.LoadScene(Global.G_SCENE_NAME_01);
        ResultStorage.Instance.SelectGame(0);
    }
    #endregion

}
