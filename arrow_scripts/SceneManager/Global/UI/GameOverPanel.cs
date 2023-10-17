using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameOverPanel : MonoBehaviour, IPointerClickHandler { 
    #region ����
    private Tween _show_ani = null;
    private Tween _close_ani = null;
    #endregion

    private void Awake()
    {
        this.transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }
    private void Start()
    {
        Show();

    }

    /*���ŵǾ��� ���*/
    private void OnDestroy()
    {
        ResetAnimation();
        SceneManager.LoadScene(Global.G_SCENE_NAME_Result);
    }
    private void ResetAnimation()
    {
        _show_ani?.Kill();
        _close_ani?.Kill();
    }
    /*�޴��� ���*/
    private void Show()
    {
        ResetAnimation();
        _show_ani = this.transform.DOScale(Vector3.one, 0.25f).SetUpdate(true).SetAutoKill();
    }
    /*�޴��� �ݱ�*/
    private void Close()
    {
        ResetAnimation();
        _close_ani = this.transform.DOScale(new Vector3(0.1f, 0.1f, 1), 0.25f).SetUpdate(true).SetAutoKill();
        _close_ani.onComplete = OnCompleteClose;
    }
    /*�ݱ� �ִϰ� �Ϸ�Ǿ��� ���*/
    private void OnCompleteClose()
    {
        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}
