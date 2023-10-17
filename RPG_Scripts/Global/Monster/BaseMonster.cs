using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    #region 변수
    public Animator _aniCtrl { get; set; }
    public SpriteRenderer _spriteRenderer { get; set; }
    public Vector3 _direction { get; set; }
    public float _distance { get; set; }
    public MonsterStateMachine _state_machine { get; private set; }//몬스터 상태
    
    public GameObject Shadow { get; set; }


    public bool isHited = false;
    public bool isEndAttack = false;
    public bool OnCollision = false;
    public bool isDeath = false;


    [SerializeField] private GameObject _model = null;
    [SerializeField] private GameObject _shadow = null;
    [SerializeField] private GameObject _hitbox = null;

    private Vector3 _shadow_pos;
    public bool SpriteIsFlip = false; //몬스터의 스프라이트가 제각각이라서 이런 변수를 둬서 바꿔줘야한다... 오른쪽을 보고있으면 true;
    #endregion
    #region 스텟
    protected int _hp;
    protected int _attack;
    protected int _defend;
    public int current_hp;
    protected float _move_speed;
    public float _high;
    protected int _money;
    public int Money => _money;
    public float Speed => _move_speed;
    public int _exp { get; set; }
    public int Damage { get { return Random.Range(_attack - _attack / 10, 1 + _attack + _attack / 10); } }
    #endregion
    #region 타겟의 상태
    private Vector3 _player_pos = Vector3.zero;
    //플레이어의 포지션을 어떻게 가져올까....
    private string hit_player_name = null;
    public string hit_player_Name => hit_player_name;
    private string _hitString = null;
    public string Hit => _hitString;
    public bool PlayerisFlip = false;
    #endregion

    #region 함수
    public virtual void Awake()
    {
        Shadow = _shadow;
        _shadow_pos = _shadow.transform.localPosition;
        _direction = Vector3.zero;
        _aniCtrl = _model.GetComponent<Animator>();
        _spriteRenderer = _model.GetComponent<SpriteRenderer>();
    }
    public virtual void Start()
    {
        InitStateMachine();
    }
    public virtual void Update()
    {
        if (Time.timeScale != 0)
        {
            _state_machine?.UpdateState();
            SetTargetPos();
            SetDirection();
            SetHitBox();
            MonsterDontOutScreenSize();
            ChangeMonsterState();
            if (isDeath)
                MonsterIsDeath();
        }
    }
    public void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            _state_machine?.FixedUpdateState();
        }
    }
    private void InitStateMachine()
    {
        _state_machine = new MonsterStateMachine(MONSTER_STATE.IDLE, new MonsterIdleState(this));
        _state_machine.AddState(MONSTER_STATE.TRACE, new MonsterTraceState(this));
        _state_machine.AddState(MONSTER_STATE.ATTACK, new MonsterAttackState(this));
        _state_machine.AddState(MONSTER_STATE.HIT, new MonsterHitState(this));
    }
    private void SetDirection()
    {
        float v = _player_pos.z-this.transform.localPosition.z;
        float h = _player_pos.x-this.transform.localPosition.x;

        _direction = new Vector3(h, 0, v).normalized;
        _distance = new Vector3(h, 0, v).magnitude;
    }
    private void SetHitBox()
    {
        _hitbox.transform.localScale = (_spriteRenderer.flipX) ? new Vector3(-1, 1, 1) : Vector3.one;
    }
    //따라갈 타겟(player)의 포지션을 받아온다.
    private void SetTargetPos()
    {
        _player_pos = SceneManager05._player_position;
        _player_pos.y = 0;
    }
    private void ChangeMonsterState()
    {
        Vector3 pos = Vector3.zero;
        pos.x = this.transform.localPosition.x;
        pos.z = this.transform.localPosition.z;
        
        if (_distance > 800f&&!isHited)
        {
            _state_machine.ChangeState(MONSTER_STATE.IDLE);
        }
        else if(_distance < 150f&&!isHited)
        {
            _state_machine.ChangeState(MONSTER_STATE.ATTACK);
        }
        else if(_distance <= 800f && _distance >= 150f&&!isHited)
        {
            _state_machine.ChangeState(MONSTER_STATE.TRACE);
        }
        if (OnCollision)
        {
            _state_machine.ChangeState(MONSTER_STATE.HIT);
            OnCollision = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            _hitString = "PlayerAttack";
            hit_player_name = other.transform.parent.parent.name;
            PlayerisFlip = (other.transform.localScale.x == -1);
            OnCollision = true;
        }
        else if (other.gameObject.CompareTag("Arrow"))
        {
            _hitString = "Arrow";
            PlayerisFlip = (other.transform.localScale.x == -1);
            OnCollision = true;
        }
        else if (other.gameObject.CompareTag("BlackHole"))
        {
            _hitString = "BlackHole";
            PlayerisFlip = (other.transform.localScale.x == -1);
            OnCollision = true;
        }
    }
    private void MonsterDontOutScreenSize()
    {
        float player_x = Mathf.Clamp(this.transform.localPosition.x, Global.object_left_x, Global.object_right_x);
        float player_z = Mathf.Clamp(this.transform.localPosition.z, Global.object_down_z, Global.object_up_z);
        this.transform.localPosition = new Vector3(player_x, this.transform.localPosition.y, player_z);
        _shadow.transform.localPosition = new Vector3(_shadow_pos.x + player_x, _shadow.transform.localPosition.y, _shadow_pos.z + player_z);
    }
    private void MonsterIsDeath()
    {
        Debug.Log("몬스터가 죽었습니다.");
        
    }
    #endregion
}
