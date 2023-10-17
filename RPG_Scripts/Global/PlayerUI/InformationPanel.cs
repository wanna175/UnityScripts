using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InformationPanel : MonoBehaviour
{
    #region 변수
    private Queue<Text> _text_inactive;
    private Color color;
    private int count;
    #endregion

    #region 함수
    private void Awake()
    {
        color = Color.white;
        count = this.transform.childCount;
        _text_inactive = new Queue<Text>();
        for(int i=0;i<count;i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            _text_inactive.Enqueue(this.transform.GetChild(i).GetComponent<Text>()); 
        }
        
    }
    public void AlertInfo(string name,string id)
    {
        switch (id)
        {
            case "item":SetText(name); break;
            case "exp":SetText("경험치" + name); break;
            case "money":SetText(name + "골드");break;
            default:Debug.Log("셋팅 오류...");break;
        }
    }
    private void SetText(string name)
    {
        if (_text_inactive.Count<=0)
        {
            Text _object = this.transform.GetChild(count - 1).gameObject.GetComponent<Text>();
            _object.DOKill();
            StopCoroutine(ActiveInfo(_object));
            _object.gameObject.SetActive(false);
            _text_inactive.Enqueue(_object);
        }
        Text _txt = _text_inactive.Dequeue();
        _txt.color = color;
        _txt.text = $"{name}을(를) 얻었습니다.";
        StartCoroutine(ActiveInfo(_txt));
    }
    private IEnumerator ActiveInfo(Text txt)
    {
        txt.gameObject.SetActive(true);
        txt.transform.SetAsFirstSibling();
        txt.DOFade(0, 2.0f);
        yield return new WaitForSeconds(2.0f);
        txt.gameObject.SetActive(false);
        _text_inactive.Enqueue(txt);
    }
    #endregion
}
