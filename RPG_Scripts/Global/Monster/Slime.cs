using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseMonster
{
    public override void Awake()
    {
        base.Awake();
        _hp = 300;
        _attack = 10;
        _defend = 10;
        current_hp = _hp;
        _exp = 100;
        _move_speed = 120f;
        _high = -100f;
        _money = 100;
    }
}
