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
        txts[0].text = $"���� : {PlayerManager.Instance._Status.LV}";//����
        txts[1].text = $"���� : " + PlayerManager.Instance._Player.name.Substring(7);
        txts[2].text = $"ü�� : {PlayerManager.Instance._Status.Current_hp} / {PlayerManager.Instance._Status.HP}";//ü��
        txts[3].text = $"����ġ : {PlayerManager.Instance._Status.Current_Exp/PlayerManager.Instance._Status.Total_Exp*100:0.00}%";//����ġ
        txts[4].text = $"���ݷ� : {PlayerManager.Instance._Status.Total_Attack}";//���ݷ�
        txts[5].text = $"���� : {PlayerManager.Instance._Status.Total_Defend}";//����
    }
    public void OnExitBtn()
    {
        this.gameObject.SetActive(false);
    }


}
