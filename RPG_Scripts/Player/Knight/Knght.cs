using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knght : BasePlayer
{
    #region 스텟

    #endregion

    #region
    public override void Awake()
    {
        base.Awake();
        _dashTime = 0.6f;
        _attack_speed = 0.4f;
        _end_combo_wait_time = 0.5f;
        _keydown_wait_time = _attack_speed + 0.3f;
    }
    //스킬을 구현한다.
    public override void SetSkill(int _num)
    {
        switch (_num)
        {
            case 0: _skill01(); break;
            case 1: _skill05(); break;
            case 2: _skill03(); break;
            case 3: _skill04(); break;
            case 4: _skill02(); break;
        }
    }
    private void _skill01() {//발도
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 0f);
        PlayerManager.Instance._Status.SetDamage(1000);
        StartCoroutine(_skill_timer(3.1f));
    }
    private IEnumerator _skill_timer(float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayerManager.Instance._Status.SetDamage(-1);
        _skill_num = -1;
        PlayerManager.Instance.isSkill = _skill_num;
        yield return 0;
    }
    private void _skill02() {//힐
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 1f);
        PlayerManager.Instance._Status.SetHealth((int)(PlayerManager.Instance._Status.HP / 10), false);
        StartCoroutine(_skill_timer(2f));
    }
    private void _skill03() {//방어력 증가
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 2f);
        StartCoroutine(_skill_timer(1.4f));
    }
    private void _skill04() {//공격력 증가
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 3f);
        StartCoroutine(_skill_timer(1.6f));
    }
    private void _skill05()//3번때리기
    {
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 4f);
        PlayerManager.Instance._Status.SetDamage(500);
        StartCoroutine(_skill_timer(1.1f));
    }
    #endregion
}
