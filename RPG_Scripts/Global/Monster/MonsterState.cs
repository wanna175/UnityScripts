using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MONSTER_STATE
{
    IDLE = 0,
    TRACE,
    ATTACK,
    HIT
}
public abstract class BaseMonsterState
{
    #region 프로퍼티
    protected BaseMonster _monster { get; private set; }
    #endregion
    #region 생성자
    public BaseMonsterState(BaseMonster monster)
    {
        this._monster = monster;
    }

    #endregion

    #region abstract 함수
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();//물리관련업데이트
    public abstract void OnExitState();
    #endregion
}
public class MonsterStateMachine
{
    #region 프로퍼티
    public BaseMonsterState _current_state { get; private set; }
    #endregion

    #region 변수
    private Dictionary<MONSTER_STATE, BaseMonsterState> _states_dic = new Dictionary<MONSTER_STATE, BaseMonsterState>();
    #endregion

    #region 생성자
    public MonsterStateMachine(MONSTER_STATE stateName, BaseMonsterState state)
    {
        AddState(stateName, state);
        _current_state = GetState(stateName);
    }
    #endregion

    #region 함수
    public void AddState(MONSTER_STATE stateName, BaseMonsterState state)
    {
        if (!_states_dic.ContainsKey(stateName))
            _states_dic.Add(stateName, state);
    }
    public BaseMonsterState GetState(MONSTER_STATE stateName)
    {
        if (_states_dic.TryGetValue(stateName, out BaseMonsterState state))
            return state;
        return null;
    }
    public void DeleteState(MONSTER_STATE stateName)
    {
        if (_states_dic.ContainsKey(stateName))
            _states_dic.Remove(stateName);
    }
    public void ChangeState(MONSTER_STATE next_stateName)
    {
        _current_state?.OnExitState();//현재 상태를 종료
        if (_states_dic.TryGetValue(next_stateName, out BaseMonsterState next_state))
            _current_state = next_state;
        _current_state?.OnEnterState();
    }
    public void UpdateState()
    {
        _current_state?.OnUpdateState();
    }
    public void FixedUpdateState()
    {
        _current_state?.OnFixedUpdateState();
    }
    #endregion
}
public class MonsterIdleState : BaseMonsterState
{
    #region 생성자
    public MonsterIdleState(BaseMonster monster) : base(monster) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _monster._aniCtrl.SetInteger("State", (int)MONSTER_STATE.IDLE);

    }

    public override void OnExitState()
    {
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnUpdateState()
    {
    }
    #endregion
}
public class MonsterTraceState : BaseMonsterState
{
    #region 생성자
    public MonsterTraceState(BaseMonster monster) : base(monster) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _monster._aniCtrl.SetInteger("State", (int)MONSTER_STATE.TRACE);

    }

    public override void OnExitState()
    {
    }

    public override void OnFixedUpdateState()
    {
        _monster.gameObject.transform.localPosition += _monster._direction * _monster.Speed * Time.deltaTime;
        _monster.Shadow.transform.localPosition += _monster._direction * _monster.Speed * Time.deltaTime;
    }

    public override void OnUpdateState()
    {
        int sprite_flip = 1;
        if (_monster.SpriteIsFlip)
            sprite_flip = -1;
        if (_monster._direction.x*sprite_flip < 0)
            _monster._spriteRenderer.flipX = false;
        else if (_monster._direction.x*sprite_flip > 0)
            _monster._spriteRenderer.flipX = true;
    }
    #endregion
}
public class MonsterAttackState : BaseMonsterState
{
    #region 변수
    private float _time;
    #endregion
    #region 생성자
    public MonsterAttackState(BaseMonster monster) : base(monster) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _monster._aniCtrl.SetInteger("State", (int)MONSTER_STATE.ATTACK);
    }

    public override void OnExitState()
    {
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnUpdateState()
    {
    }
    #endregion
}
public class MonsterHitState : BaseMonsterState
{
    private float _time;
    private bool isDeath;
    #region 생성자
    public MonsterHitState(BaseMonster monster) : base(monster) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _monster.isHited = true;
        _time = 0.0f;

        int atk = PlayerManager.Instance._Status.Damage;
        int damage = Random.Range(atk - atk / 10, atk + atk / 10);
        bool critical = (Random.Range(1, 11) < 2) ? true : false;
        if (critical) damage += damage;
        if (_monster.Hit.Equals("PlayerAttack"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage, critical);

            _deff.transform.position = _monster.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);

            _eff.gameObject.SetActive(true);
            _eff.transform.position = _monster.transform.position +Vector3.up*_monster._high;
            var name = _monster.hit_player_Name;
            switch (name)
            {
                case "Wizard":
                    if (PlayerManager.Instance.isSkill == -1 || PlayerManager.Instance.isSkill == 3)
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
                    if (PlayerManager.Instance.isSkill == -1 || PlayerManager.Instance.isSkill == 0)
                        name = Global.Hit_effect02;
                    else
                        name = Global.Hit_effect05;
                    break;
            }
            _eff.PlayAni(name, _monster.PlayerisFlip);
        }
        else if (_monster.Hit.Equals("Arrow"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage, critical);

            _deff.transform.position = _monster.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);

            _eff.gameObject.SetActive(true);
            _eff.transform.position = _monster.transform.position + Vector3.up * _monster._high;

            _eff.PlayAni(Global.Hit_effect02, _monster.PlayerisFlip);
        }
        else if (_monster.Hit.Equals("BlackHole"))
        {
            var _eff = EffectManager.Instance.Get_Object();
            damage = _eff.BlackHoleEnter(damage);
            var _deff = DamageManager.Instance.Get_Damage_Effect(damage, true);

            _deff.transform.position = _monster.transform.position + Vector3.up * 100;
            _deff.gameObject.SetActive(true);

            _eff.gameObject.SetActive(true);
            _eff.transform.position = _monster.transform.position + Vector3.up * _monster._high;

            _eff.PlayAni(Global.Hit_effect01, _monster.PlayerisFlip);
        }
        if (_monster.current_hp - damage > 0)
        {
            _monster.current_hp -= damage; 
        }
        else
        {
            isDeath = true;
            _monster.isDeath = true;
            _monster.current_hp = 0;
        }
        _monster._aniCtrl.SetTrigger("isHited");
        _monster._aniCtrl.SetFloat("Hit", (isDeath) ? 1 : 0);

    }

    public override void OnExitState()
    {
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnUpdateState()
    {
        _time += Time.deltaTime;
        if (_time > 0.5f&&!isDeath)
            _monster.isHited = false;
    }
    #endregion
}

