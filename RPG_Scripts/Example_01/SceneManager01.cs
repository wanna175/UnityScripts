using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager01 : SceneManager_parent
{
    public enum BUTTON
    {
        NONE = -1,
        START,
        CONTINUE,
        SETTING,
        EXIT
    };
    #region ������Ƽ
    public override string SceneName => Global.START_SCENE;
    #endregion

    #region ����
    public static BUTTON isButtonClick;

    [SerializeField] private GameObject Title = null;
    [SerializeField] private GameObject _title_txt = null;
    private Animator _title_Ani = null;
    #endregion

    #region �Լ�
    public override void Awake()
    {
        base.Awake();
        isButtonClick = BUTTON.NONE;
        _title_Ani = Title.GetComponent<Animator>();
    }
    public override void Start()
    {
        base.Start();

    }
    public override void Update()
    {
        base.Update();
        if (isButtonClick != BUTTON.NONE)
            OnbuttonClick();
        
    }
    //������ ��ư�� ������ ���
    private void OnbuttonClick()
    {
        _title_Ani.SetBool("isEnd", true);
        _title_txt.gameObject.SetActive(false);
 
        switch (isButtonClick)
        {
            case BUTTON.CONTINUE:
                break;
            case BUTTON.START:
                break;
            case BUTTON.SETTING:break;
            case BUTTON.EXIT:break;
        }
    }
   
    #endregion
}
