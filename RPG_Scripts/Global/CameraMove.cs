using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject _player = null;
    [SerializeField] Transform _playerRoot = null;
    [SerializeField] Vector3 _camera_offset = Vector3.zero;
    #region 함수
    private void Awake()
    {
        
        
    }
    private void Start()
    {
        //_player = PlayerManager.Instance._Player.GetComponent<BasePlayer>();//이놈은 그냥 원본 프리팹일 뿐이다...
        _player = _playerRoot.GetComponentInChildren<BasePlayer>().gameObject;
        CameraUpdata(false);

    }

    private void FixedUpdate()
    {
        CameraUpdata(true);
    }
    //카메라 업데이트
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
    //따라다닐 객체를 셋팅한다.
    public void SetTarget(GameObject obj)
    {
        _player = obj;
    }
    #endregion
}
