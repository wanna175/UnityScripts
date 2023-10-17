using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : BasePlayer
{
    public override void Awake()
    {
        base.Awake();
        _dashTime = 0.7f;
        _attack_speed = 0.5f;
        _end_combo_wait_time = 0.7f;
        _keydown_wait_time = _attack_speed + 0.2f;
    }
    //��ų�� �����Ѵ�.
    public override void SetSkill(int _num)
    {
        switch (_num)
        {
            case 0: _skill01(); break;
            case 1: _skill02(); break;
            case 2: _skill03(); break;
            case 3: _skill04(); break;
            default: _skill_num = -1; break;
        }
    }
    private void _skill01()
    {//��Ȧ
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 0f);
        StartCoroutine(_skill_timer(1.5f));
    }
    private IEnumerator _skill_timer(float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayerManager.Instance._Status.SetDamage(-1);
        _skill_num = -1;
        PlayerManager.Instance.isSkill = _skill_num;
        yield return 0;
    }
    private void _skill02()
    {//�㸮���� �ַο�
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        PlayerManager.Instance._Status.SetDamage(500);
        _aniCtrl.SetFloat("Skill", 1f);
        StartCoroutine(_skill_timer(1.2f));
    }
    private void _skill03()
    {//�˹�
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 2f);
        StartCoroutine(_skill_timer(2.1f));
    }
    private void _skill04()
    {//���ݷ� ����,���� ����
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        PlayerManager.Instance._Status.SetDamage(500);
        _aniCtrl.SetFloat("Skill", 3f);
        StartCoroutine(_skill_timer(1f));
    }
}
