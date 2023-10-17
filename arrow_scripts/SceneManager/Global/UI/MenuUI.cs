using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour, IPointerClickHandler
{
    #region 변수
    private Tween _show_ani = null;
    private Tween _close_ani = null;
    private bool isSceneMove = false;

    #endregion

    private void Awake()
    {
        this.transform.localScale = new Vector3(0.1f, 0.1f,1);
    }
    private void Start()
    {
        Show();
        
    }

    /*제거되었을 경우*/
    private void OnDestroy()
    {
        ResetAnimation();
        if(isSceneMove)
            LoadingSceneManager.LoadScene(Global.G_SCENE_NAME_START);
    }
    private void ResetAnimation()
    {
        _show_ani?.Kill();
        _close_ani?.Kill();
    }
    /*메뉴판 출력*/
    private void Show()
    {
        ResetAnimation();
        _show_ani = this.transform.DOScale(Vector3.one, 0.25f).SetUpdate(true).SetAutoKill();
    }
    /*메뉴판 닫기*/
    private void Close()
    {
        ResetAnimation();
        _close_ani = this.transform.DOScale(new Vector3(0.1f,0.1f,1), 0.25f).SetUpdate(true).SetAutoKill();
        _close_ani.onComplete = OnCompleteClose;
    }
    /*닫기 애니가 완료되었을 경우*/
    private void OnCompleteClose()
    {
        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        string name = eventData.pointerCurrentRaycast.gameObject.name;
        switch (name)
        {
            case Global.MENU_START: Close(); break;
            case Global.MENU_RESTART: Close(); isSceneMove = true; break;
            case Global.MENU_SETTING: break;
            case Global.MENU_MARTKET: break;
            case Global.MENU_EXIT: break;
            case Global.MENU_SOCIAL: break;
        }
    }
}
