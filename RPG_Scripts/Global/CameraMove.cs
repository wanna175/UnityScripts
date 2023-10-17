using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject _player = null;
    [SerializeField] Transform _playerRoot = null;
    [SerializeField] Vector3 _camera_offset = Vector3.zero;
    #region �Լ�
    private void Awake()
    {
        
        
    }
    private void Start()
    {
        //_player = PlayerManager.Instance._Player.GetComponent<BasePlayer>();//�̳��� �׳� ���� �������� ���̴�...
        _player = _playerRoot.GetComponentInChildren<BasePlayer>().gameObject;
        CameraUpdata(false);

    }

    private void FixedUpdate()
    {
        CameraUpdata(true);
    }
    //ī�޶� ������Ʈ
    private void CameraUpdata(bool isLerp)
    {
        float camera_z = Mathf.Clamp(_player.transform.position.z + -_camera_offset.z, -824, -558);
        float camera_x = Mathf.Clamp(_player.transform.position.x, -1500, 1500);
        Vector3 camera_pos = new Vector3(camera_x, _camera_offset.y+PlayerManager.Instance.current_floor*1500, camera_z);
        if (isLerp)
            this.transform.position = Vector3.Lerp(this.transform.position, camera_pos, 0.15f);
        else
            this.transform.position = camera_pos;
        
        
    }
    //����ٴ� ��ü�� �����Ѵ�.
    public void SetTarget(GameObject obj)
    {
        _player = obj;
    }
    #endregion
}
