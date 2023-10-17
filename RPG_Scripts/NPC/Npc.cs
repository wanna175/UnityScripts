using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Npc : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject _dialog = null;
    private TextMeshPro _Text = null;
    private GameObject _txtBox = null;
    private Queue<string> _dialog_txt;
    private bool isTalk = false;
    #endregion

    #region 함수
    private void Awake()
    {
        _dialog_txt = new Queue<string>();
        ReadCSV();
        _Text = _dialog.transform.GetComponentInChildren<TextMeshPro>();
        _txtBox = _Text.transform.GetChild(0).gameObject;
    }
    public abstract void ClickNpc();
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!isTalk)
        {
            StartCoroutine(_Ondialog());
        }
    }
    private IEnumerator _Ondialog()
    {
        yield return new WaitForEndOfFrame();
        _dialog.SetActive(true);
        _Text.text = _dialog_txt.Dequeue();
        isTalk = true;
        yield return new WaitForSeconds(2.0f);
        _dialog.SetActive(false);
        isTalk = false;
        _dialog_txt.Enqueue(_Text.text);
    }
    //csv파일에서 npc의 대사를 읽어오자...
    private void ReadCSV()
    {
        TextAsset _txt = Resources.Load("file/npc_txt") as TextAsset;
        var lines = _txt.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            var str = lines[i].Split(',');
            
            if (str[0].Equals(this.gameObject.name))
            {
                _dialog_txt.Enqueue(str[1]);
                _dialog_txt.Enqueue(str[2]);
                _dialog_txt.Enqueue(str[3]);
                return;
            }
        }
    }
    #endregion
}
