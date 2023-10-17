using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//기본 상태 클래스
public enum STATE
{
    IDLE = 0,
    WALK,
    RUN,
    JUMP,
    ATTACK,
    DASH,
    HIT,
    SKILL
}
public abstract class BaseState
{
    #region 프로퍼티
    protected BasePlayer _player { get; private set; }
    #endregion
    #region 생성자
    public BaseState(BasePlayer player) {
        this._player = player;
    }

    #endregion

    #region abstract 함수
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();//물리관련업데이트
    public abstract void OnExitState();
    #endregion
}
public class StateMachine
{
    #region 프로퍼티
    public BaseState _current_state { get; private set; }
    #endregion

    #region 변수
    private Dictionary<STATE, BaseState> _states_dic = new Dictionary<STATE, BaseState>();
    #endregion

    #region 생성자
    public StateMachine(STATE stateName,BaseState state)
    {
        AddState(stateName, state);
        _current_state = GetState(stateName);
    }
    #endregion

    #region 함수
    public void AddState(STATE stateName,BaseState state){
        if (!_states_dic.ContainsKey(stateName))
            _states_dic.Add(stateName, state);
    }
    public BaseState GetState(STATE stateName){
        if (_states_dic.TryGetValue(stateName, out BaseState state))
            return state;
        return null;
    }
    public void DeleteState(STATE stateName) {
        if (_states_dic.ContainsKey(stateName))
            _states_dic.Remove(stateName);
    }
    public void ChangeState(STATE next_stateName)
    {
        _current_state?.OnExitState();//현재 상태를 종료
        if (_states_dic.TryGetValue(next_stateName, out BaseState next_state))
            _current_state = next_state;
        _current_state?.OnEnterState();
    }
    public void UpdateState() {
        _current_state?.OnUpdateState();
    }
    public void FixedUpdateState(){
        _current_state?.OnFixedUpdateState();
    }
    #endregion
}

public class IdleState : BaseState
{
    #region 생성자
    public IdleState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _player._aniCtrl.SetInteger("State", (int)STATE.IDLE);

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

public class WalkState : BaseState
{
    #region 생성자
    public WalkState(BasePlayer player) : base(player){}
    #endregion
    #region 함수
    public override void OnEnterState(){
        _player._aniCtrl.SetInteger("State", (int)STATE.WALK);
    }

    public override void OnExitState(){

    }

    public override void OnFixedUpdateState(){
        _player.gameObject.transform.localPosition += _player._direction * 700.0f * Time.deltaTime;
        _player.Shadow.transform.localPosition += _player._direction * 700.0f * Time.deltaTime;
    }

    public override void OnUpdateState(){
        if (_player._direction.x < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._direction.x > 0)
            _player._spriteRenderer.flipX = false;
    }
    #endregion
}
public class RunState : BaseState
{
    #region 생성자
    public RunState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _player._aniCtrl.SetInteger("State", (int)STATE.RUN);
    }

    public override void OnExitState()
    {

    }

    public override void OnFixedUpdateState()
    {
        _player.gameObject.transform.localPosition += _player._direction * 1400.0f * Time.deltaTime;
        _player.Shadow.transform.localPosition += _player._direction * 1400.0f * Time.deltaTime;
    }

    public override void OnUpdateState()
    {
        if (_player._direction.x < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._direction.x > 0)
            _player._spriteRenderer.flipX = false;
    }
    #endregion
}

public class JumpState : BaseState
{
    #region 생성자
    public JumpState(BasePlayer player) : base(player) { }
    #endregion

    #region 함수
    public override void OnEnterState()
    {
        _player._aniCtrl.SetTrigger("isJump");
        _player._aniCtrl.SetInteger("State", (int)STATE.JUMP);
        _player._rigidbody.AddForce(Vector3.up * 4000f,ForceMode.Impulse);
        _player.isJump = true;
    }

    public override void OnExitState()
    {
    }

    public override void OnFixedUpdateState()
    {
        if (_player._rigidbody.velocity.y < 0)
        {
            _player._aniCtrl.ResetTrigger("isJump");
            _player._aniCtrl.SetTrigger("isDown");
        }

        if (_player.transform.localPosition.y < -10+PlayerManager.Instance.map_num*1500)
        {
            _player._aniCtrl.ResetTrigger("isDown");
        }
        _player.gameObject.transform.localPosition += new Vector3(_player._direction.x * 700.0f * Time.deltaTime,0,0);
        _player.Shadow.transform.localPosition+= new Vector3(_player._direction.x * 700.0f * Time.deltaTime, 0, 0);
    }

    public override void OnUpdateState()
    {
        if (_player._direction.x < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._direction.x > 0)
            _player._spriteRenderer.flipX = false;

    }
    #endregion
}

public class AttackState : BaseState
{
    #region 변수
    public float _time;
    private float comboCount;
    private bool isEndCombo;
    private float _attack_speed;
    private float end_combo_wait_time;
    private float _keydown_wait_time;
    #endregion
    #region 생성자
    public AttackState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _attack_speed = _player.AttackSpeed;
        end_combo_wait_time = _player.EndComboWait;
        _keydown_wait_time = _player.KeyDownWait;
        isEndCombo = false;
        _player.isAttack = true;
        if (_player.isJump)
        {
            _player._rigidbody.velocity = Vector3.zero;
            Physics.gravity = new Vector3(0, Global._gravity / 100, 0);
        }
        comboCount = (_player.isJump) ? 3 : 0;
        _player._aniCtrl.SetInteger("State", (int)STATE.ATTACK);
        _player._aniCtrl.SetFloat("Combo", comboCount);
        _time = 0.0f;
    }

    public override void OnExitState()
    {
        Physics.gravity = new Vector3(0, Global._gravity, 0);
        _player.isAttack = false;
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnUpdateState()
    {
        _time += Time.deltaTime;
        if (isEndCombo)
            return;

        if (!_player.isJump)
        {
            if (_time > _keydown_wait_time)
            {
                _player.isAttack = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && _time > _attack_speed && comboCount < 1f)
            {
                comboCount += 0.5f;
                _player._aniCtrl.SetFloat("Combo", comboCount);
                _player._aniCtrl.Play("ComboAttack", -1, 0f);
                _time = 0.0f;
            }
            if (comboCount >= 1f)
            {
                isEndCombo = true;
                _player.StartCoroutine(EndCombo());
            }
        }
        else
        {
            if (_time > _keydown_wait_time)
            {
                Physics.gravity = new Vector3(0, Global._gravity, 0);
                _player._aniCtrl.SetFloat("Combo", 6);
                
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && _time > _attack_speed && comboCount < 3.5f)
            {
                comboCount += 0.5f;
                _player._aniCtrl.SetFloat("Combo", comboCount);
                _player._aniCtrl.Play("ComboAttack", -1, 0f);
                _time = 0.0f;
            }
            if (comboCount >= 3.5f)
            {
                isEndCombo = true;
                _player.StartCoroutine(EndJumpCombo());
            }
        }
    }
    private IEnumerator EndCombo()
    {
        yield return new WaitForSeconds(end_combo_wait_time+0.5f);
        _player.isAttack = false;
    }
    private IEnumerator EndJumpCombo()
    {
        yield return new WaitForSeconds(end_combo_wait_time);
        _player._aniCtrl.SetFloat("Combo", 6);
        Physics.gravity = new Vector3(0, Global._gravity, 0);
    }
    #endregion
}
public class DashState : BaseState
{
    #region 변수
    private float _time;
    private float _dashTime;
    #endregion

    #region 생성자
    public DashState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _dashTime = _player.DashTime;
        _player.isDash = true;
        _player._aniCtrl.SetInteger("State", (int)STATE.DASH);
        _player._aniCtrl.SetFloat("Dash", 0);
        _time = 0.0f;
        if (_player.isJump)
        {
            _player._rigidbody.velocity = Vector3.zero;
            Physics.gravity = new Vector3(0, Global._gravity / 100, 0);
        }
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

        if (_time > _dashTime&&!_player.isJump)
        {
            _player.isDash = false;
        }
        if (_time > _dashTime&&_player.isJump)
        {
            Physics.gravity = new Vector3(0, Global._gravity, 0);
            _player._aniCtrl.SetFloat("Dash", 0.5f);
        }

    }
    #endregion
}

public class HitState : BaseState
{
    #region 변수
    private float _time;
    private float _hitTime;
    private float _deathTime;
    private bool isDeath;
    #endregion

    #region 생성자
    public HitState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        isDeath = false;
        _hitTime = 0.5f;
        _deathTime = 2.48f;
        _player.isHit = true;
        var _deff = DamageManager.Instance.Get_Damage_Effect(_player._hit_damage, false,true);
        _deff.transform.position = _player.transform.position + Vector3.up * 100;
        _deff.gameObject.SetActive(true);
        if (PlayerManager.Instance._Status.Current_hp - _player._hit_damage > 0)
        {//적이 가진 데미지를 받아와야함 나중에
            PlayerManager.Instance._Status.SetHealth(_player._hit_damage, true);
        }
        else
        {
            isDeath = true;
            PlayerManager.Instance._Status.SetHealth(-1,true);
        }
        _player._aniCtrl.SetInteger("State", (int)STATE.HIT);
        _player._aniCtrl.SetFloat("Hit", (isDeath) ? 1:0);

        _time = 0.0f;
    }

    public override void OnExitState()
    {
        _player.OnCollision = false;
        _player.isDeath = isDeath;
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnUpdateState()
    {
        _time += Time.deltaTime;

        if (!isDeath&&_time > _hitTime )
        {
            _player.isHit = false;
        }
        else if (isDeath && _time > _deathTime){
            _player.isHit = false;
        }

    }
    #endregion
}
public class SKillState : BaseState
{
    #region 변수
    #endregion
    #region 생성자
    public SKillState(BasePlayer player) : base(player) { }
    #endregion
    #region 함수
    public override void OnEnterState()
    {
        _player.SetSkill(_player._skill_num);
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
