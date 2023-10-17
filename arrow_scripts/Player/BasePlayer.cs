//#define COMPUTER
#define MOBILE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasePlayer : MonoBehaviour
{
    #region ������Ƽ, ����
    public Animator _aniCtrl  { get; set; }//�÷��̾� �ִ�
    public SpriteRenderer _spriteRenderer { get; set; }
    public StateMachine _state_machine { get; private set; }//�÷��̾� ����
    public Vector3 _direction { get; set; }//�÷��̾� ����
    public Sprite _skill_image;
    public float Skill_time { get { return _skill_time; } }

    protected float _skill_time = 0;
    public float CurrentCoolTime { get { return _current_cool_time; } }
    protected float _current_cool_time = 0f;
    #endregion

    #region ����
    public int HP { get { return _hp; } }
    public float Speed { get { return _speed; } }
    
    protected int _hp = 1;
    protected float _speed = 1500.0f;
    #endregion

    #region �Լ�
    public virtual void Awake(){
        _direction = Vector3.zero;
    }
    public virtual void  Start() {
        InitStateMachine();
    }
    public virtual void Update(){
        if (Time.timeScale != 0){
            _state_machine?.UpdateState();
            ChangePlayerState();
        }
    
    }
    public virtual void FixedUpdate() {
        _state_machine?.FixedUpdateState();
    }
    public abstract void SetSkill();
    private void InitStateMachine()
    {
        _state_machine = new StateMachine(STATE.IDLE, new IdleState(this));
        _state_machine.AddState(STATE.WALK, new WalkState(this));
        _state_machine.AddState(STATE.JUMP, new JumpState(this));
        _state_machine.AddState(STATE.SKILL, new SKillState(this));
    }
    //�÷��̾� ���¸� �ٲ۴�.
    public virtual void ChangePlayerState()
    {
        if (_direction != Vector3.zero)
            _state_machine.ChangeState(STATE.WALK);
        else _state_machine.ChangeState(STATE.IDLE);

#if COMPUTER
        if (_current_cool_time == 0 && Input.GetKeyDown(KeyCode.Space))
            _state_machine.ChangeState(STATE.SKILL);
#elif MOBILE
            if (_current_cool_time == 0 && PlayerSkillUI.isClick)
                _state_machine.ChangeState(STATE.SKILL);
#endif
    }

    #endregion
}
