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
    #region ������Ƽ
    public override string SceneName => Global.G_SCENE_NAME_02;
    #endregion

    #region ����
    ///���� �÷��� 
    private GAME_STATE current_state = GAME_STATE.PLAY;
    [SerializeField] private ArrowPool_0102 _arrowPool = null;
    [SerializeField] private CoinPool _coinPool = null;
    private BasePlayer _player = null;
    [SerializeField] private Transform PlayerRoot = null;
    [SerializeField] List<Transform> list;//�� �������� ����Ʈ


    //������ 
    [SerializeField] private Joystick_panel _joystick;
    [SerializeField] private Image _playerUI;
    [SerializeField] private Text _timeUI;
    [SerializeField] private Transform _GameOverPanelRoot;
    [SerializeField] private GameObject _GameOverPanel;
    private float _sucess_time;
    #endregion

    #region �Լ�
    public override void Awake(){
        base.Awake();
        current_state = GAME_STATE.PLAY;
        _sucess_time = 0;
        Arrow_0102.isOver = false;
    }
    public override void Start(){
        base.Start();
        _player = Instantiate(PlayerManager.Instance.GetPlayer(), PlayerRoot).GetComponent<BasePlayer>();//���� ���õ� �÷��̾� ����
        _playerUI.sprite = _player._skill_image;
        StartCoroutine(shotting_arrow());
        StartCoroutine(Game_over());//�ٵ� �̷��� �ڷ�ƾ ���� ������ ���� ���Ȱɸ���...
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
    //ȭ����ġ ������ȯ
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
    //ȭ�찴ü�� ������ �����Ѵ�.
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
    //��ų ��Ÿ���� ��ٸ���.
    private void wait_cool_time() {
        _playerUI.fillAmount = _player.CurrentCoolTime / _player.Skill_time;
    }
    //�Է¹��� ������ ������ �����Ѵ�.
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
    //�÷��̾� �̵����� �����Ѵ�.
    private void PlayerDontOutScreenSize()
    {
        float x = Mathf.Clamp(_player.transform.localPosition.x, -860, 860);
        float y = Mathf.Clamp(_player.transform.localPosition.y, -400, 400);
        _player.transform.localPosition = new Vector3(x, y, 0);
    }
    //������ ���¸� �����Ѵ�.
    private void Change_UI_State()
    {
        _sucess_time += Time.deltaTime;
        _timeUI.text = $"{_sucess_time:0.00}";
    }
    //������ �����ϰ��
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
