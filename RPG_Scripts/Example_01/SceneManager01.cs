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
    #region 프로퍼티
    public override string SceneName => Global.START_SCENE;
    #endregion

    #region 변수
    public static BUTTON isButtonClick;

    [SerializeField] private GameObject Title = null;
    [SerializeField] private GameObject _title_txt = null;
    private Animator _title_Ani = null;
    #endregion

    #region 함수
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
    //씬에서 버튼이 눌렸을 경우
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
