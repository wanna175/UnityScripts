using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�⺻ ���� Ŭ����
public enum STATE
{
    IDLE = 0,
    WALK,
    JUMP,
    SKILL
}
public abstract class BaseState
{
    #region ������Ƽ
    protected BasePlayer _player { get; private set; }
    #endregion
    #region ������
    public BaseState(BasePlayer player) {
        this._player = player;
    }

    #endregion

    #region abstract �Լ�
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();//�������þ�����Ʈ
    public abstract void OnExitState();
    #endregion
}
public class StateMachine
{
    #region ������Ƽ
    public BaseState _current_state { get; private set; }
    #endregion

    #region ����
    private Dictionary<STATE, BaseState> _states_dic = new Dictionary<STATE, BaseState>();
    #endregion

    #region ������
    public StateMachine(STATE stateName,BaseState state)
    {
        AddState(stateName, state);
        _current_state = GetState(stateName);
    }
    #endregion

    #region �Լ�
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
        _current_state?.OnExitState();//���� ���¸� ����
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
    #region ������
    public IdleState(BasePlayer player) : base(player) { }
    #endregion
    #region �Լ�
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
    #region ������
    public WalkState(BasePlayer player) : base(player){}
    #endregion
    #region �Լ�
    public override void OnEnterState(){
        _player._aniCtrl.SetInteger("State", (int)STATE.WALK);
    }

    public override void OnExitState(){
    }

    public override void OnFixedUpdateState(){
    }

    public override void OnUpdateState(){
        if (_player._direction.x < 0)
            _player._spriteRenderer.flipX = true;
        else if (_player._direction.x > 0)
            _player._spriteRenderer.flipX = false;
        _player.gameObject.transform.localPosition += _player._direction * _player.Speed * Time.deltaTime;
    }
    #endregion
}

public class JumpState : BaseState
{
    #region ������
    public JumpState(BasePlayer player) : base(player) { }
    #endregion
    #region �Լ�
    public override void OnEnterState()
    {
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

public class SKillState : BaseState
{
    #region ����
    Vector3 _direction;
    #endregion
    #region ������
    public SKillState(BasePlayer player) : base(player) { }
    #endregion
    #region �Լ�
    public override void OnEnterState()
    {
        _player.SetSkill();
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
