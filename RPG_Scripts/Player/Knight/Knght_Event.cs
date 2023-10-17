using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knght_Event : MonoBehaviour
{
    [SerializeField] private BasePlayer _player = null;
    private Effect _effect;
  
    #region 애니매이션 이벤트 함수
    //3단공격이벤트 처리
    public void OnFrontAttak()
    {
        float _moveX = (_player._spriteRenderer.flipX) ? -100.0f : 100.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.2f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.2f);
    }
    public void EndFrontAttack()
    {
        float _moveX = (_player._spriteRenderer.flipX) ? 100.0f : -100.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.3f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.3f);
    }
    //대쉬이벤트 처리
    public void OnDashEnter()
    {
        bool isFlip = _player._spriteRenderer.flipX;
        if (!_player.isJump)
        {
            var _eff = EffectManager.Instance.Get_Object();
            _eff.gameObject.SetActive(true);
            _eff.transform.localPosition = _player.transform.localPosition + Vector3.up * -50;
            if(_player._spriteRenderer.flipX) 
            _eff.PlayAni(Global.dash_effect,isFlip);
        }
        float _moveX = isFlip ? -1000.0f : 1000.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.55f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.55f);
    }
    //달리기 이벤트 처리
    public void OnRunEnter()
    {
        var _eff = EffectManager.Instance.Get_Object();
        _eff.gameObject.SetActive(true);
        _eff.transform.localPosition = _player.transform.localPosition + Vector3.up * -150;
        _eff.PlayAni(Global.run_effect,false);
    }
    //Hited 이벤트 처리
    public void OnHitedEnter()
    {
        var _eff = EffectManager.Instance.Get_Object();
        _eff.gameObject.SetActive(true);
        _eff.transform.localPosition = _player.transform.localPosition;
        _eff.PlayAni(Global.Hit_effect01, false);

        float _moveX = (_player._spriteRenderer.flipX) ? 200.0f : -200.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.15f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.15f);
    }
    //죽었을시 애니메이션 멈추기
    public void OnDeathEnd()
    {
        Time.timeScale = 0.0f;
    }

    //skill 1 이벤트
    public void OnSkill_01_Start()
    {
        bool isFlip = _player._spriteRenderer.flipX;
        float _moveX = isFlip ? -1000.0f : 1000.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.15f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.15f);
    }
    public void OnSkill_01_end()
    {

        _player._spriteRenderer.flipX = !_player._spriteRenderer.flipX;
     
        bool isFlip = _player._spriteRenderer.flipX;
        float _moveX = isFlip ? -1000.0f : 1000.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.15f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.15f);
    }

    public void OnSkill_05()
    {
        bool isFlip = _player._spriteRenderer.flipX;
        float moveX = (isFlip)? -300:300;
        var _eff = EffectManager.Instance.Get_Object();
        _eff.gameObject.SetActive(true);
        _eff.transform.localPosition = _player.transform.localPosition+Vector3.right*moveX;
        _eff.PlayAni(Global.Skill_effect01, isFlip);
    }
    
    #endregion

}
