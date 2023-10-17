using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    #region 프로퍼티
    public static float ScreenWidth
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.pixelWidth;
#else
            return Screen.Width
#endif
        }
    }
    public static float ScreenHeight
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.pixelHeight;
#else
            return Screen.Height;
#endif
        }
    }
    #endregion

    #region 변수
    private Camera _camera = null;
    [SerializeField] GameObject scalingTarget = null;
    #endregion

    #region 함수
    //초기화
    public void Awake()
    {
        _camera = GetComponent<Camera>();
        SetUpCamera();

    }
    public void Start()
    {
        SetUpScalingTarget();
    }
    //카메라를 설정한다.
    private void SetUpCamera()
    {
        _camera.orthographic = true;
        _camera.orthographicSize = Global.G_DESIGN_HEIGHT / 2.0f;
        _camera.transform.localRotation = Quaternion.AngleAxis(30, Vector3.right);//카메라를 x축으로 45돌린다.
        _camera.transform.localPosition = new Vector3(0, 1000, -1000);
        _camera.farClipPlane = 5000;
    }
    //현재 디바이스의 해상도에 맞춰서 오브젝트를 스케일한다.
    private void SetUpScalingTarget()
    {
        //디바이스의 해상도
        float _myRatio = Global.G_DESIGN_WIDTH / Global.G_DESIGN_HEIGHT;
        float ratio_width = ScreenHeight * _myRatio;
        if (ScreenWidth < ratio_width - float.Epsilon)
            scalingTarget.transform.localScale = Vector3.one * (ScreenWidth / ratio_width);
    }
    #endregion
}
