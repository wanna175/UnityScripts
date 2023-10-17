#define MOBILE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_02 : BasePlayer
{
    #region 변수
    #endregion
    #region 함수
    public override void Awake()//Awake할때 파일로 읽어오든 뭐든 스텟을 업데이트 해야한다.
    {
        base.Awake();
        _aniCtrl = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _skill_time = 5.0f;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void SetSkill()
    {
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetBool("Skill", true);
        _spriteRenderer.flipX = _direction.x < 0 ? true : false;
        _spriteRenderer.color = new Color(255, 0, 0);
        _speed = 2000;
#if MOBILE
        PlayerSkillUI.isClick = false;
#endif
        StartCoroutine(timer());
    }
    private IEnumerator timer()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        yield return new WaitForSeconds(3.0f);
        _aniCtrl.SetBool("Skill", false);
        _spriteRenderer.color = Color.white;
        _speed = 1500.0f;
        while (_current_cool_time < _skill_time)
        {
            _current_cool_time += Time.deltaTime;
            yield return wait;
        }
        _current_cool_time = 0.0f;
    }
    #endregion
}
