using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool_0102 : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject _arrow_prefab = null;
    private Queue<Arrow_0102> pooling_queue = new Queue<Arrow_0102>();
    #endregion

    #region 함수
    //초기화
    private void Awake(){
        Initialize_pool(10);
    }
    //초기값으로 큐에 10만큼 미리 생성해놓음=>렉줄이기
    private void Initialize_pool(int count)
    {
        for(int i = 0; i < count; i++)
        {
            pooling_queue.Enqueue(Create_object());
        }
    }
    //새로운 객체를 생성한다.
    private Arrow_0102 Create_object()
    {
        var obj = Instantiate(_arrow_prefab,this.transform).GetComponent<Arrow_0102>();
        obj.gameObject.SetActive(false);
        return obj;
    }
    //큐에 존재하는 객체를 가져온다.
    public Arrow_0102 Get_Object()
    {
        var obj = (pooling_queue.Count <= 0) ? Create_object() : pooling_queue.Dequeue();
        return obj;
    }
    public void Return_Object(Arrow_0102 obj)
    {
        obj.gameObject.SetActive(false);
        pooling_queue.Enqueue(obj);
    }
    #endregion
}
