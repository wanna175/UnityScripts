using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_0102 : MonoBehaviour
{
    #region 변수
    private ArrowPool_0102 _arrowPool = null;
    private Rigidbody2D _Rigidbody2D = null;
    private float _speed = 50000.0f;
    public static bool isOver = false;
    #endregion

    #region 함수
    private void Awake()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _arrowPool = this.gameObject.GetComponentInParent<ArrowPool_0102>();
    }
    //객체를 초기화한다.
    public void Set_arrow(Vector3 _start,Vector3 _destination)
    {
        Vector2 arrow_destination = (_destination - _start).normalized;
     
        this.transform.localPosition = _start;
        
        float dot = Vector2.Dot(Vector2.right, arrow_destination);
        float degree = Mathf.Acos(dot) * Mathf.Rad2Deg;
        this.transform.localEulerAngles = (arrow_destination.y < 0) ? new Vector3(0, 0, -degree) : new Vector3(0, 0, degree);

        this.gameObject.SetActive(true);
        _Rigidbody2D.AddForce(arrow_destination*_speed);
        
    }
    //객체를 큐에서 반환한다.
    public void Return_arrow()
    {
        _arrowPool.Return_Object(this);
    }
    //충돌했을 경우
    private void OnTriggerEnter2D(Collider2D other)
    {
        //화면 밖 벽에 부딪힌 경우
        if (other.CompareTag("E01Wall")) Return_arrow();
        //플레이어와 부딪힌 경우
        if (other.CompareTag("Player"))
        {
            isOver = true;
        }
    }
    #endregion
}
