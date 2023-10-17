using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : BasePlayer
{
    public Queue<Arrow> pooling_queue = new Queue<Arrow>();//아이템 풀링
    public static GameObject _arrow_prefab = null;
    public static Transform _arrow_parent = null;
    public override void Awake()
    {
        base.Awake();
        _dashTime = 0.6f;
        _attack_speed = 0.4f;
        _end_combo_wait_time = 0.5f;
        _keydown_wait_time = _attack_speed + 0.3f;
        _arrow_prefab = Resources.Load<GameObject>("Prefabs/Player/Arrow/arrow");
        _arrow_parent = this.transform.parent;
        Initalize_pool(5);
    }
    //스킬을 구현한다.
    public override void SetSkill(int _num)
    {
        switch (_num)
        {
            case 0: _skill01(); break;
            case 1: _skill02(); break;
            case 2: _skill03(); break;
            case 3: _skill04(); break;
            default: _skill_num = -1;break;
        }
    }
    private void _skill01()
    {//화살비
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 0f);
        PlayerManager.Instance._Status.SetDamage(500);
        StartCoroutine(_skill_timer(2.6f));
    }
    private IEnumerator _skill_timer(float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayerManager.Instance._Status.SetDamage(-1);
        _skill_num = -1;
        PlayerManager.Instance.isSkill = _skill_num;
        yield return 0;
    }
    private void _skill02()
    {//허리케인 애로우
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        PlayerManager.Instance._Status.SetDamage(1000);
        _aniCtrl.SetFloat("Skill", 1f);
        StartCoroutine(_skill_timer(1.6f));
    }
    private void _skill03()
    {//넉백
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 2f);
        StartCoroutine(_skill_timer(0.95f));
    }
    private void _skill04()
    {//공격력 증가,방어력 증가
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 3f);
        StartCoroutine(_skill_timer(4.6f));
    }


    #region 오브젝트 풀링 관련
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //새로운 객체를 생성한다.
    public Arrow Create_object()
    {
        var obj = Instantiate(_arrow_prefab,_arrow_parent).GetComponent<Arrow>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //큐에 존재하는 객체를 가져온다.
    public Arrow Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_object(Arrow obj)
    {
        obj.spriteRenderer.flipX = false;
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
