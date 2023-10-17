using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : BasePlayer
{
    public Queue<Arrow> pooling_queue = new Queue<Arrow>();//������ Ǯ��
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
    //��ų�� �����Ѵ�.
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
    {//ȭ���
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
    {//�㸮���� �ַο�
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        PlayerManager.Instance._Status.SetDamage(1000);
        _aniCtrl.SetFloat("Skill", 1f);
        StartCoroutine(_skill_timer(1.6f));
    }
    private void _skill03()
    {//�˹�
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 2f);
        StartCoroutine(_skill_timer(0.95f));
    }
    private void _skill04()
    {//���ݷ� ����,���� ����
        _aniCtrl.SetInteger("State", (int)STATE.SKILL);
        _aniCtrl.SetFloat("Skill", 3f);
        StartCoroutine(_skill_timer(4.6f));
    }


    #region ������Ʈ Ǯ�� ����
    private void Initalize_pool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //���ο� ��ü�� �����Ѵ�.
    public Arrow Create_object()
    {
        var obj = Instantiate(_arrow_prefab,_arrow_parent).GetComponent<Arrow>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //ť�� �����ϴ� ��ü�� �����´�.
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
