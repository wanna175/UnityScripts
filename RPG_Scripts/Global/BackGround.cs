using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    #region ����
    private GameObject _player = null;

    [SerializeField] private GameObject _playerRoot = null;
    [SerializeField] private GameObject _middle = null;
    [SerializeField] private GameObject _end = null;
    #endregion

    #region �Լ�
    private void Start()
    {
        _player = _playerRoot.GetComponentInChildren<BasePlayer>().gameObject;
    }
    private void FixedUpdate()
    {
        Vector3 pos = new Vector3(Camera.main.transform.position.x, this.transform.position.y, this.transform.position.z);
        _end.transform.position = pos;
        float back_x = Mathf.Clamp(_player.transform.localPosition.x, -1500, 1500);
        _middle.transform.localPosition = new Vector3(1194+back_x/3f, _middle.transform.localPosition.y, _middle.transform.localPosition.z);
    }
    //������ �� ��ü �����Ѵ�.
    public void SetTarget(GameObject obj)
    {
        _player = obj;
    }
    #endregion
}
