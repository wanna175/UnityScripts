using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_0102 : MonoBehaviour
{
    #region ����
    private ArrowPool_0102 _arrowPool = null;
    private Rigidbody2D _Rigidbody2D = null;
    private float _speed = 50000.0f;
    public static bool isOver = false;
    #endregion

    #region �Լ�
    private void Awake()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _arrowPool = this.gameObject.GetComponentInParent<ArrowPool_0102>();
    }
    //��ü�� �ʱ�ȭ�Ѵ�.
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
    //��ü�� ť���� ��ȯ�Ѵ�.
    public void Return_arrow()
    {
        _arrowPool.Return_Object(this);
    }
    //�浹���� ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ȭ�� �� ���� �ε��� ���
        if (other.CompareTag("E01Wall")) Return_arrow();
        //�÷��̾�� �ε��� ���
        if (other.CompareTag("Player"))
        {
            isOver = true;
        }
    }
    #endregion
}
