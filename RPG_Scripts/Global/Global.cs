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

    //화면에서 보일수 있는 위치의 값..
    public const float object_right_x = 2300f;
    public const float object_left_x = -2300f;
    public const float object_up_z = 250f;
    public const float object_down_z = -500f;
    #endregion

    #region 물리관련 변수
    public const float _gravity = -10000;
    #endregion

    #region 씬이름
    public const string START_SCENE = "Example_01 (시작씬)";
    public const string LOADING_SCENE = "Example_02 (로딩씬)";
    public const string PLAYER_SELECT_SCENE = "Example_03 (캐릭터 선택)";
    public const string SCENE_NAME_04 = "Example_04 (시작마을)";
    public const string SCENE_NAME_05 = "Example_05 (던전 - 초원)";
    #endregion

    #region 이펙트 애니관련..
    public const string run_effect = "effect_running";
    public const string dash_effect = "effect_dash";
    public const string Hit_effect01 = "effect_hit";
    public const string Hit_effect02 = "effect_arrow_hit";
    public const string Hit_effect03 = "effect_fire_hit";
    public const string Hit_effect04 = "effect_knight_skill_hit1";
    public const string Hit_effect05 = "effect_archer_skill_hit2";
    public const string Hit_effect06 = "effect_wizard_skill_hit2";
    public const string Hit_effect07 = "effect_wizard_skill_hit4";
    public const string Skill_effect01 = "effect_knight_skill_1";
    public const string Skill_effect02 = "effect_archer_skill_1";
    public const string Skill_effect03 = "effect_archer_skill_2";
    public const string Skill_effect04 = "effect_wizard_skill_1";
    #endregion

    #region 몬스터 id
    public const int SLIME_ID = 0;
    #endregion
}
