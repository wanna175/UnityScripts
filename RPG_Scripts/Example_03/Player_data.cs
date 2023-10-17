using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_data
{
    public GameObject _player;//플레이어 프리팹
    public Player_Item _item;
    public Status _status;

    public Player_data(GameObject _player,Player_Item _item,Status _status)
    {
        this._player = _player;
        this._item = _item;
        this._status = _status;
    }
}
