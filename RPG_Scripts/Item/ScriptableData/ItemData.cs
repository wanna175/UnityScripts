using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public string Grade => _grade;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;
    public int Price => _price;

    [SerializeField] private int _id;
    [SerializeField] private string _name;    // 아이템 이름
    [SerializeField] private string _grade;
    [SerializeField] private int _price;
    [Multiline]
    [SerializeField] private string _tooltip; // 아이템 설명
    [SerializeField] private Sprite _iconSprite; // 아이템 아이콘
    [SerializeField] private GameObject _dropItemPrefab;// 바닥에 떨어졌을때 생성할 프리펩

    //아이템을 생성한다. 여기서 생성하는 이유는 Item클래스에서 생성시 모든 아이템마다 이 함수를 들고 있게되므로 모든 아이템이
    //참조하고 있는 ItemData에서만 가능하도록 해주려는 것이다.
    public abstract Item CreateItem();

}
