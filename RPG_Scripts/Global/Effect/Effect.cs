using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator _effAni = null;
    private SpriteRenderer spriteRenderer = null;
    private void Awake()
    {
        _effAni = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    //애니메이션이 다 재생이 되었을 때 이벤트
    public void EndEffect()
    {
        spriteRenderer.sprite = null;
        EffectManager.Instance.Return_object(this);
        spriteRenderer.flipX = false;
    }
    public int BlackHoleEnter(int damage)
    {
        return damage * 5;
    }
    public void PlayAni(string ani_name,bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
        switch (ani_name)
        {
            case Global.run_effect:
                _effAni.SetInteger("select" ,0);
                _effAni.SetFloat("normal_Effect", 1);
                break;
            case Global.dash_effect:
                _effAni.SetInteger("select", 0);
                _effAni.SetFloat("normal_Effect", 0);
                break;
            case Global.Hit_effect01:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", Random.Range(0, 1.5f));
                break;
            case Global.Hit_effect02:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 3);
                break;
            case Global.Hit_effect03:
                spriteRenderer.flipX = (isFlip) ? false : true;
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 2);
                break;
            case Global.Hit_effect04:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 4);
                break;
            case Global.Hit_effect05:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 5);
                break;
            case Global.Hit_effect06:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 6);
                break;
            case Global.Hit_effect07:
                _effAni.SetInteger("select", 1);
                _effAni.SetFloat("hited_Effect", 7);
                break;
            case Global.Skill_effect01:
                _effAni.SetInteger("select", 2);
                _effAni.SetFloat("skill_Effect", 0);
                break;
            case Global.Skill_effect02:
                _effAni.SetInteger("select", 2);
                _effAni.SetFloat("skill_Effect", 1);
                break;
            case Global.Skill_effect03:
                _effAni.SetInteger("select", 2);
                _effAni.SetFloat("skill_Effect", 2);
                break;
            case Global.Skill_effect04:
                _effAni.SetInteger("select", 2);
                _effAni.SetFloat("skill_Effect", 3);
                break;
        }
    }
}
