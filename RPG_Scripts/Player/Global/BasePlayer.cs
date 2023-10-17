
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasePlayer : MonoBehaviour
{
    #region 프로퍼티, 변수
    public Animator _aniCtrl  { get; set; }//플레이어 애니
    public Rigidbody _rigidbody { get; set; }
    public SpriteRenderer _spriteRenderer { get; set; }
    public StateMachine _state_machine { get; private set; }//플레이어 상태
    public Vector3 _direction { get; set; }//플레이어 방향
    public GameObject Shadow { get; set; }


    [SerializeField] private GameObject _model = null;
    [SerializeField] private GameObject _shadow = null;
    [SerializeField] private GameObject _hitbox = null;

    //더블입력을 위한 변수
    private bool _singlePressed = false;
    private bool _doublePressed = false;
    private float _doublePressThreshold = 0.3f;
    private float _lastKeyDownTime;

    public bool isJump { get; set; } = false;
    public bool isAttack { get; set; } = false;
    public bool isDash { get; set; } = false;
    public bool isHit { get; set; } = false;
    public bool OnCollision { get; set; } = false;
    public bool isDeath { get; set; } = false;
    public int _skill_num= -1;
    public int _hit_damage; //몬스터가 가지고 있는 데미지 정보
    #endregion

    #region 애니메이션 셋팅 변수
    public float DashTime { get { return _dashTime; } }
    public float AttackSpeed { get { return _attack_speed; } }
    public float EndComboWait { get { return _end_combo_wait_time; } }
    public float KeyDownWait { get { return _keydown_wait_time; } }

    protected float _dashTime;
    protected float _attack_speed;
    protected float _end_combo_wait_time;
    protected float _keydown_wait_time;
    private Vector3 _shadow_pos;


    
    //캐릭터 이동 제한
    #endregion

    #region 함수
    public virtual void Awake(){
        isJump = false;
        Shadow = _shadow;
        _shadow_pos = _shadow.transform.localPosition;
        _direction = Vector3.zero;
        _rigidbody = this.GetComponent<Rigidbody>();
        _aniCtrl = _model.GetComponent<Animator>();
        _spriteRenderer = _model.GetComponent<SpriteRenderer>();
    }
    public virtual void  Start() {
        InitStateMachine();
    }
    public virtual void Update(){
        if (Time.timeScale != 0){
            _state_machine?.UpdateState();
            SetDirection();
            SetHitBox();
            PlayerDontOutScreenSize();
            ChangePlayerState();
            if (isDeath)
                PlayerIsDeath();
        }
    }
    public virtual void FixedUpdate() {
        if (Time.timeScale != 0)
        {
            _state_machine?.FixedUpdateState();
        }
    }
    private void InitStateMachine()
    {
        _state_machine = new StateMachine(STATE.IDLE, new IdleState(this));
        _state_machine.AddState(STATE.WALK, new WalkState(this));
        _state_machine.AddState(STATE.RUN, new RunState(this));
        _state_machine.AddState(STATE.JUMP, new JumpState(this));
        _state_machine.AddState(STATE.ATTACK, new AttackState(this));
        _state_machine.AddState(STATE.DASH, new DashState(this));
        _state_machine.AddState(STATE.HIT, new HitState(this));
        _state_machine.AddState(STATE.SKILL, new SKillState(this));
    }
    //더블입력체크및 방향을 설정한다.
    private void SetDirection()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _doublePressed = (Time.time - _lastKeyDownTime < _doublePressThreshold);
            _lastKeyDownTime = Time.time;
        }
        if (_doublePressed) _singlePressed = false;
        else _singlePressed = true;

        _direction = new Vector3(h, 0, v).normalized;
        
    }
    private void SetHitBox()
    {
        _hitbox.transform.localScale = (_spriteRenderer.flipX) ? new Vector3(-1, 1, 1) : Vector3.one;
    }
    //플레이어 상태를 바꾼다.
    public virtual void ChangePlayerState()
    {
        if (isDeath)
            return;
        if (Input.GetKeyDown(KeyCode.LeftAlt)&&!isJump&&!isAttack&&!isDash&&!isHit&&_skill_num==-1)
        {
            _state_machine.ChangeState(STATE.JUMP);
        }
        if (_direction != Vector3.zero&&!isJump&&!isAttack&&!isDash&&!isHit&&_skill_num==-1) {
            if (_singlePressed)
            {
                _state_machine.ChangeState(STATE.WALK);
            }
            else if (_doublePressed)
            {
                _state_machine.ChangeState(STATE.RUN);
            }
        }
        if(_direction==Vector3.zero&&!isJump&&!isAttack&&!isDash&&!isHit&&_skill_num==-1)
            _state_machine.ChangeState(STATE.IDLE);

        if (Input.GetKeyDown(KeyCode.LeftControl)&&!isAttack&&!isDash&&!isHit&&_skill_num==-1)
            _state_machine.ChangeState(STATE.ATTACK);

        if (Input.GetKeyDown(KeyCode.Space) && !isAttack && !isDash&&!isHit&&_skill_num==-1)
            _state_machine.ChangeState(STATE.DASH);

        if (!isDash && !isHit && !isJump&&_skill_num==-1)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _skill_num = 0;
                _state_machine.ChangeState(STATE.SKILL);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                _skill_num = 1;
                _state_machine.ChangeState(STATE.SKILL);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                _skill_num = 2;
                _state_machine.ChangeState(STATE.SKILL);
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                _skill_num = 3;
                _state_machine.ChangeState(STATE.SKILL);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _skill_num = 4;
                _state_machine.ChangeState(STATE.SKILL);
            }
            PlayerManager.Instance.isSkill = _skill_num;
        }
        if (OnCollision && !isHit&&!isAttack!&&!isDash)
            _state_machine.ChangeState(STATE.HIT);
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isJump && other.gameObject.CompareTag("Floor"))
        {
            isJump = false;
            isAttack = false;
            isDash = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("EnemyAttack")&&!isDash&&!isAttack&&_skill_num==-1)
        {
            _hit_damage = other.transform.parent.parent.GetComponent<BaseMonster>().Damage;
            OnCollision = true;
        }
    }
    //플레이어 이동범위 제한한다.
    private void PlayerDontOutScreenSize()
    {
        float player_x = Mathf.Clamp(this.transform.localPosition.x, Global.object_left_x, Global.object_right_x);
        float player_z = Mathf.Clamp(this.transform.localPosition.z, Global.object_down_z, Global.object_up_z);
        this.transform.localPosition = new Vector3(player_x, this.transform.localPosition.y, player_z);
        _shadow.transform.localPosition = new Vector3(_shadow_pos.x+player_x, _shadow.transform.localPosition.y,_shadow_pos.z+ player_z);
    }
    private void PlayerIsDeath()
    {
        Debug.Log("플레이어가 죽었습니다.");
    }
    public abstract void SetSkill(int _num);
    #endregion
}
