using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region ����
    private CoinPool _coinPool = null;
    private Rigidbody2D _Rigidbody2D = null;
    private float _speed = 70000.0f;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _coinPool = this.gameObject.GetComponentInParent<CoinPool>();
    }
    //��ü�� �ʱ�ȭ�Ѵ�.
    public void Set_coin(Vector3 _start, Vector3 _destination)
    {
        Vector2 coin_destination = (_destination - _start).normalized;

        this.transform.localPosition = _start;

        this.gameObject.SetActive(true);
        _Rigidbody2D.AddForce(coin_destination * _speed);

    }
    //��ü�� ť���� ��ȯ�Ѵ�.
    public void Return_coin()
    {
        _coinPool.Return_Object(this);
    }
    //�浹���� ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("E01Wall")) Return_coin();
        //�÷��̾�� �ε��� ���
        if (other.CompareTag("Player"))
        {
            Return_coin();
            ResultStorage.Instance.Coin++;
        }
    }
    #endregion
}
