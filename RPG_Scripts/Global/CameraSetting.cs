using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    #region ������Ƽ
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

    #region ����
    private Camera _camera = null;
    [SerializeField] GameObject scalingTarget = null;
    #endregion

    #region �Լ�
    //�ʱ�ȭ
    public void Awake()
    {
        _camera = GetComponent<Camera>();
        SetUpCamera();

    }
    public void Start()
    {
        SetUpScalingTarget();
    }
    //ī�޶� �����Ѵ�.
    private void SetUpCamera()
    {
        _camera.orthographic = true;
        _camera.orthographicSize = Global.G_DESIGN_HEIGHT / 2.0f;
        _camera.transform.localRotation = Quaternion.AngleAxis(30, Vector3.right);//ī�޶� x������ 45������.
        _camera.transform.localPosition = new Vector3(0, 1000, -1000);
        _camera.farClipPlane = 5000;
    }
    //���� ����̽��� �ػ󵵿� ���缭 ������Ʈ�� �������Ѵ�.
    private void SetUpScalingTarget()
    {
        //����̽��� �ػ�
        float _myRatio = Global.G_DESIGN_WIDTH / Global.G_DESIGN_HEIGHT;
        float ratio_width = ScreenHeight * _myRatio;
        if (ScreenWidth < ratio_width - float.Epsilon)
            scalingTarget.transform.localScale = Vector3.one * (ScreenWidth / ratio_width);
    }
    #endregion
}
