using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMan : BaseMonster
{
    public override void Awake()
    {
        base.Awake();
        _hp = 700;
        _attack = 20;
        _defend = 20;
        current_hp = _hp;
        _exp = 200;
        _move_speed = 150f;
        _high = 0f;
        _money = 100;
        SpriteIsFlip = true;
    }
}
