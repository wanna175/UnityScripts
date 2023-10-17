//#define COMPUTER
#define MOBLIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneManager0102 : SceneManager_parent
{
    enum GAME_STATE
    {
        PLAY = 0,
        EXIT
    };
    #region 프로퍼티
    public override string SceneName => Global.G_SCENE_NAME_02;
    #endregion

    #region 변수
    ///게임 플레이 
    private GAME_STATE current_state = GAME_STATE.PLAY;
    [SerializeField] private ArrowPool_0102 _arrowPool = null;
    [SerializeField] private CoinPool _coinPool = null;
    private BasePlayer _player = null;
    [SerializeField] private Transform PlayerRoot = null;
    [SerializeField] List<Transform> list;//벽 받으려는 리스트


    //유아이 
    [SerializeField] private Joystick_panel _joystick;
    [SerializeField] private Image _playerUI;
    [SerializeField] private Text _timeUI;
    [SerializeField] private Transform _GameOverPanelRoot;
    [SerializeField] private GameObject _GameOverPanel;
    private float _sucess_time;
    #endregion

    #region 함수
    public override void Awake(){
        base.Awake();
        current_state = GAME_STATE.PLAY;
        _sucess_time = 0;
        Arrow_0102.isOver = false;
    }
    public override void Start(){
        base.Start();
        _player = Instantiate(PlayerManager.Instance.GetPlayer(), PlayerRoot).GetComponent<BasePlayer>();//현재 선택된 플레이어 생성
        _playerUI.sprite = _player._skill_image;
        StartCoroutine(shotting_arrow());
        StartCoroutine(Game_over());//근데 이렇게 코루틴 많이 돌려도 겜이 렉안걸리나...
    }
    public override void Update()
    {
        base.Update();
        _player._direction = SetDirection();
        PlayerDontOutScreenSize();
        Change_UI_State();
        if (_player.CurrentCoolTime != 0)
            wait_cool_time();
        Game_over();

    }
    //화살위치 랜덤반환
    private Vector3 Rand_pos()
    {
        int idx = Random.Range(0, 4);
        Transform wall = list[idx];
        Vector3 pos = Vector3.zero;
        switch (idx)
        {
            case 0:
                pos.x = wall.localPosition.x+300;
                pos.y = Random.Range(-600, 600);
                break;
            case 1:
                pos.x = wall.localPosition.x-300;
                pos.y = Random.Range(-600, 600);
                break;
            case 2:
                pos.y = wall.localPosition.y-300;
                pos.x = Random.Range(-1000, 1000);
                break;
            case 3:
                pos.y = wall.localPosition.y+300;
                pos.x = Random.Range(-1000, 1000);
                break;
        }
        return pos;
    }
    //화살객체를 벽에서 생성한다.
    private IEnumerator shotting_arrow()
    {
        WaitForSeconds seconds = new WaitForSeconds(1.0f);
        yield return seconds;
        do {
            yield return seconds;
            int isCoin = Random.Range(0, 5);
            if (isCoin == 1)
            {
                var obj = _coinPool.Get_Object();
                obj.Set_coin(Rand_pos(), _player.transform.localPosition);
            }
            else
            {
                var obj = _arrowPool.Get_Object();
                obj.Set_arrow(Rand_pos(), _player.transform.localPosition);
            }
        } while (current_state == GAME_STATE.PLAY);
        yield break;
    }
    //스킬 쿨타임을 기다린다.
    private void wait_cool_time() {
        _playerUI.fillAmount = _player.CurrentCoolTime / _player.Skill_time;
    }
    //입력받은 값으로 방향을 설정한다.
    private Vector3 SetDirection()
    {
#if COMPUTER
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
#elif MOBLIE
        float h = _joystick.Horizontal;
        float v = _joystick.Vertical;
#endif
        return new Vector3(h, v, 0).normalized;
    }
    //플레이어 이동범위 제한한다.
    private void PlayerDontOutScreenSize()
    {
        float x = Mathf.Clamp(_player.transform.localPosition.x, -860, 860);
        float y = Mathf.Clamp(_player.transform.localPosition.y, -400, 400);
        _player.transform.localPosition = new Vector3(x, y, 0);
    }
    //유아이 상태를 변경한다.
    private void Change_UI_State()
    {
        _sucess_time += Time.deltaTime;
        _timeUI.text = $"{_sucess_time:0.00}";
    }
    //게임이 오버일경우
    private IEnumerator Game_over()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while (!Arrow_0102.isOver)
        {
            yield return wait;
        }
        Time.timeScale= 0;
        ResultStorage.Instance.Score = _sucess_time;
        ResultStorage.Instance.Process_game_score();
        Instantiate(_GameOverPanel, _GameOverPanelRoot);
        
    }
#endregion
}
