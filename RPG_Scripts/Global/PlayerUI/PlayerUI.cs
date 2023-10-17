using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region ����
    [SerializeField] private Image _hp_bar = null;
    #endregion

    #region �Լ�
    private void FixedUpdate()
    {
        float _fill = PlayerManager.Instance._Status.Current_hp / PlayerManager.Instance._Status.HP;
        _hp_bar.fillAmount = Mathf.Lerp(_hp_bar.fillAmount, _fill, Time.deltaTime * 10f);
    }
    #endregion
}
