using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wizard_Event : MonoBehaviour
{
    [SerializeField] private BasePlayer _player = null;
    //�뽬�̺�Ʈ ó��
    public void OnDashEnter()
    {
        float _moveX = (_player._spriteRenderer.flipX) ? -1000.0f : 1000.0f;
        float X = Mathf.Clamp(_player.transform.localPosition.x + _moveX, -2300, 2300);
        _player.transform.DOLocalMoveX(X, 0.55f);
        _player.Shadow.transform.DOLocalMoveX(X, 0.55f);
    }

    //Hited �̺�Ʈ ó��
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
    //�׾����� �ִϸ��̼� ���߱�
    public void OnDeathEnd()
    {
        Time.timeScale = 0.0f;
    }
    public void OnSkill_01()
    {
        bool isFlip = _player._spriteRenderer.flipX;
        float moveX = (isFlip) ? -500 : 500;
        var _eff = EffectManager.Instance.Get_Object();
        _eff.gameObject.SetActive(true);
        _eff.transform.localPosition = _player.transform.localPosition + Vector3.right * moveX;
        _eff.PlayAni(Global.Skill_effect04, isFlip);
    }
    public void OnSkill_04()
    {
        bool isFlip = _player._spriteRenderer.flipX;
        float moveX = (isFlip) ? -500 : 500;
        var _eff = EffectManager.Instance.Get_Object();
        _eff.gameObject.SetActive(true);
        _eff.transform.localPosition = _player.transform.localPosition + Vector3.right * moveX;
        _eff.PlayAni(Global.Hit_effect07, isFlip);
    }
}
