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

    //ȭ�鿡�� ���ϼ� �ִ� ��ġ�� ��..
    public const float object_right_x = 2300f;
    public const float object_left_x = -2300f;
    public const float object_up_z = 250f;
    public const float object_down_z = -500f;
    #endregion

    #region �������� ����
    public const float _gravity = -10000;
    #endregion

    #region ���̸�
    public const string START_SCENE = "Example_01 (���۾�)";
    public const string LOADING_SCENE = "Example_02 (�ε���)";
    public const string PLAYER_SELECT_SCENE = "Example_03 (ĳ���� ����)";
    public const string SCENE_NAME_04 = "Example_04 (���۸���)";
    public const string SCENE_NAME_05 = "Example_05 (���� - �ʿ�)";
    #endregion

    #region ����Ʈ �ִϰ���..
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

    #region ���� id
    public const int SLIME_ID = 0;
    #endregion
}
