#define MOBILE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_01 : BasePlayer
{
    #region 변수
    
    #endregion
    #region 함수
    public override void Awake()//Awake할때 파일로 읽어오든 뭐든 스텟을 업데이트 해야한다.
    {
        base.Awake();
        _aniCtrl = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _skill_time = 3.0f;
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
        Vector3 _dash_dir = _direction * 1000f + this.transform.localPosition;
        _dash_dir.x = Mathf.Clamp(_dash_dir.x, -860, 860);
        _dash_dir.y = Mathf.Clamp(_dash_dir.y, -400, 400);
        this.transform.DOLocalMove(_dash_dir, 1.0f);
#if MOBILE
        PlayerSkillUI.isClick = false;
#endif
        StartCoroutine(timer());
    }
    private IEnumerator timer()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.7f);
        _aniCtrl.SetBool("Skill", false);
        while (_current_cool_time < _skill_time)
        {
            _current_cool_time += Time.deltaTime;
            yield return wait;
        }
        _current_cool_time = 0.0f;

    }
#endregion
}
