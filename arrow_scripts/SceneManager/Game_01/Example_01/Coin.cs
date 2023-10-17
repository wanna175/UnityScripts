using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region 변수
    private CoinPool _coinPool = null;
    private Rigidbody2D _Rigidbody2D = null;
    private float _speed = 70000.0f;
    #endregion

    #region 함수
    private void Awake()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _coinPool = this.gameObject.GetComponentInParent<CoinPool>();
    }
    //객체를 초기화한다.
    public void Set_coin(Vector3 _start, Vector3 _destination)
    {
        Vector2 coin_destination = (_destination - _start).normalized;

        this.transform.localPosition = _start;

        this.gameObject.SetActive(true);
        _Rigidbody2D.AddForce(coin_destination * _speed);

    }
    //객체를 큐에서 반환한다.
    public void Return_coin()
    {
        _coinPool.Return_Object(this);
    }
    //충돌했을 경우
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("E01Wall")) Return_coin();
        //플레이어와 부딪힌 경우
        if (other.CompareTag("Player"))
        {
            Return_coin();
            ResultStorage.Instance.Coin++;
        }
    }
    #endregion
}
