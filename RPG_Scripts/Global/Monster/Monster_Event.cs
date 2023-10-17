using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Event : MonoBehaviour
{
    [SerializeField] private GameObject _monster = null;
    private BaseMonster _baseMonster = null;
    [SerializeField] private Drop_item_Manager _item_manager = null;
    private MonsterSetting _setting = null;

    private void Awake()
    {
        _setting = this.transform.parent.parent.parent.gameObject.GetComponent<MonsterSetting>();
        _baseMonster = _monster.transform.GetChild(1).gameObject.GetComponent<BaseMonster>();
    }
    //몬스터가 죽었을때
    public void MonsterIsDead()
    {
        if ((int)Random.Range(0, 10) == 0)
            _item_manager.Drop_item_toField(this.transform.position, Global.SLIME_ID);
        _setting._monster_count--;
        _setting._infoPanel.AlertInfo(_baseMonster._exp.ToString(), "exp");
        PlayerManager.Instance._Status.SetEXP(_baseMonster._exp);
        Destroy(_monster);
    }
}
