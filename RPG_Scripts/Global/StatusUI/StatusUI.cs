using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    private Text[] txts;
    public static bool isChange;


    private void Start()
    {
        isChange = false;
        txts = this.transform.GetComponentsInChildren<Text>();
        Debug.Log(txts.Length);
        SetStatusUI();
    }
    
    private void FixedUpdate()
    {
        if (isChange)
            SetStatusUI();
    }
    private void SetStatusUI()
    {
        txts[0].text = $"레벨 : {PlayerManager.Instance._Status.LV}";//레벨
        txts[1].text = $"직업 : " + PlayerManager.Instance._Player.name.Substring(7);
        txts[2].text = $"체력 : {PlayerManager.Instance._Status.Current_hp} / {PlayerManager.Instance._Status.HP}";//체력
        txts[3].text = $"경험치 : {PlayerManager.Instance._Status.Current_Exp/PlayerManager.Instance._Status.Total_Exp*100:0.00}%";//경험치
        txts[4].text = $"공격력 : {PlayerManager.Instance._Status.Total_Attack}";//공격력
        txts[5].text = $"방어력 : {PlayerManager.Instance._Status.Total_Defend}";//방어력
    }
    public void OnExitBtn()
    {
        this.gameObject.SetActive(false);
    }


}
