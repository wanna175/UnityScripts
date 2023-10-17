using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    #region �ػ� ���� ������ �Լ�
    //ȭ�� ���� �ػ� ����
    public const float G_DESIGN_WIDTH = 1920.0f;
    public const float G_DESIGN_HEIGHT = 1080.0f;
    public static readonly Vector3 G_DESIGN_SIZE = new Vector3(G_DESIGN_WIDTH, G_DESIGN_HEIGHT, 0.0f);
    #endregion

    #region �޴��� ��ư�� �̸���
    public const string MENU_START = "start";
    public const string MENU_RESTART = "restart";
    public const string MENU_SETTING = "setting";
    public const string MENU_MARTKET = "market";
    public const string MENU_EXIT = "exit";
    public const string MENU_SOCIAL = "social";
    public const string MENU_RESULT = "result";
    #endregion


    #region Scene ���� ������ �Լ�
    //���̸�
    public const string G_SCENE_NAME_START = "Example_0000 (���۾�)";
    public const string G_SCENE_NAME_ROADING = "Example_0000 (�ε���)";
    public const string G_SCENE_NAME_00 = "Example_0001 (���� ����)";
    public const string G_SCENE_NAME_01 = "Example_0002 (ĳ���� ����)";
    public const string G_SCENE_NAME_02 = "Example_0102 (�׸���� - �÷���)";
    public const string G_SCENE_NAME_03 = "Example_0202 (��Ű�� - �÷���)";
    public const string G_SCENE_NAME_Result = "Example_0003 (�����)";
    #endregion
}
