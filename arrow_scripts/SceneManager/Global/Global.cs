using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    #region 해상도 관련 변수와 함수
    //화면 기준 해상도 변수
    public const float G_DESIGN_WIDTH = 1920.0f;
    public const float G_DESIGN_HEIGHT = 1080.0f;
    public static readonly Vector3 G_DESIGN_SIZE = new Vector3(G_DESIGN_WIDTH, G_DESIGN_HEIGHT, 0.0f);
    #endregion

    #region 메뉴판 버튼의 이름들
    public const string MENU_START = "start";
    public const string MENU_RESTART = "restart";
    public const string MENU_SETTING = "setting";
    public const string MENU_MARTKET = "market";
    public const string MENU_EXIT = "exit";
    public const string MENU_SOCIAL = "social";
    public const string MENU_RESULT = "result";
    #endregion


    #region Scene 관련 변수와 함수
    //씬이름
    public const string G_SCENE_NAME_START = "Example_0000 (시작씬)";
    public const string G_SCENE_NAME_ROADING = "Example_0000 (로딩씬)";
    public const string G_SCENE_NAME_00 = "Example_0001 (게임 선택)";
    public const string G_SCENE_NAME_01 = "Example_0002 (캐릭터 선택)";
    public const string G_SCENE_NAME_02 = "Example_0102 (죽림고수 - 플레이)";
    public const string G_SCENE_NAME_03 = "Example_0202 (쿠키런 - 플레이)";
    public const string G_SCENE_NAME_Result = "Example_0003 (결과씬)";
    #endregion
}
