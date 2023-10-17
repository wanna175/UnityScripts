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
    [SerializeField] private string _name;    // ������ �̸�
    [SerializeField] private string _grade;
    [SerializeField] private int _price;
    [Multiline]
    [SerializeField] private string _tooltip; // ������ ����
    [SerializeField] private Sprite _iconSprite; // ������ ������
    [SerializeField] private GameObject _dropItemPrefab;// �ٴڿ� ���������� ������ ������

    //�������� �����Ѵ�. ���⼭ �����ϴ� ������ ItemŬ�������� ������ ��� �����۸��� �� �Լ��� ��� �ְԵǹǷ� ��� ��������
    //�����ϰ� �ִ� ItemData������ �����ϵ��� ���ַ��� ���̴�.
    public abstract Item CreateItem();

}
