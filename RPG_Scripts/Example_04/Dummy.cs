using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    #region 변수
    [SerializeField] private Animator _dummy_ani = null;
    #endregion
    #region 함수
    private void OnTriggerEnter(Collider other)
    {
        int atk = PlayerManager.Instance._Status.Damage;
        int damage = Random.Range(atk - atk / 10, atk + atk / 10);
        bool critical = (Random.Range(1, 11) < 2) ? true : false;
        if (critical) damage += damage;
        if (other.transform.CompareTag("PlayerAttack"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage,critical);

            _deff.transform.position = this.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);
            
            _eff.gameObject.SetActive(true);
            _eff.transform.position = this.transform.position;
            var name = other.transform.parent.parent.name;
            switch (name)
            {
                case "Wizard":
                    if (PlayerManager.Instance.isSkill == -1|| PlayerManager.Instance.isSkill == 3)
                        name = Global.Hit_effect03;
                    else if (PlayerManager.Instance.isSkill == 1)
                        name = Global.Hit_effect06;
                    break;
                case "Knight":
                    if (PlayerManager.Instance.isSkill == -1)
                        name = Global.Hit_effect01;
                    else
                        name = Global.Hit_effect04;
                    break;
                case "Archer":
                    if (PlayerManager.Instance.isSkill == -1||PlayerManager.Instance.isSkill==0)
                        name = Global.Hit_effect02;
                    else
                        name = Global.Hit_effect05;
                    break;
            }
            var isFlip = (other.transform.localScale.x == -1);
            _eff.PlayAni(name, isFlip);
            _dummy_ani.SetTrigger("isHited");
        }
        else if (other.transform.CompareTag("Arrow"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage, critical);

            _deff.transform.position = this.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);

            _eff.gameObject.SetActive(true);
            _eff.transform.position = this.transform.position;

            var isFlip = (other.transform.localScale.x == -1);
            _eff.PlayAni(Global.Hit_effect02, isFlip);
            _dummy_ani.SetTrigger("isHited");
        }
        else if (other.transform.CompareTag("BlackHole"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            damage = _eff.BlackHoleEnter(damage);
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage, true);

            _deff.transform.position = this.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);

            _eff.gameObject.SetActive(true);
            _eff.transform.position = this.transform.position;

            var isFlip = (other.transform.localScale.x == -1);
            _eff.PlayAni(Global.Hit_effect01, isFlip);
            _dummy_ani.SetTrigger("isHited");
        }
    }
    #endregion
}
