using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    #region ����
    private int Level;
    private float hp;
    private int atk;
    private int total_atk;//���������� �ջ��� ��
    private int def;
    private int total_def;
    private float total_Exp;

    public int LV => Level;
    public float HP => hp;
    public int Attack => atk;
    public int Total_Attack => total_atk;
    public int Defend => def;
    public int Total_Defend => total_def;
    public float Total_Exp => total_Exp;
    


    private float current_hp;
    public float Current_hp => current_hp;
    private float current_exp;
    public float Current_Exp => current_exp;
    private int damage;
    public int Damage => damage;
    #endregion

    #region ������
    public Status()
    {
        Level = 1;
        atk = 10;
        def = 10;
        hp = 100;
        current_hp = hp;
        total_Exp = 5000;
        current_exp = 0;
        total_atk = 10;
        total_def = 10;
        damage = total_atk;
    }
    public Status(int level,int atk,int def,float exp)//����, �Ѱ�,�ѹ�, ���� ����ġ�� ���Ͽ� ��������
    {
        this.Level = level;
        this.total_atk = atk;
        this.total_def = def;
        this.current_exp = exp;
        hp = 100 * level;
        this.atk = level * 10;
        this.def = level * 10;
        this.current_hp = hp;
        this.total_Exp = level * 5000;
        this.damage = atk;
    }
    #endregion
    #region �Լ�
    public void SetAttack(int item_attack)
    {
        total_atk = atk + item_attack;
        StatusUI.isChange = true;
    }
    public void SetDefend(int item_defend)
    {
        total_def = def + item_defend;
        StatusUI.isChange = true;
    }
    public void SetHealth(int value,bool isDamege)
    {
        if(value == -1)
        {
            PlayerManager.Instance.IsDeath = true;
        }
        current_hp += (isDamege) ? -value : value;
        current_hp = Mathf.Clamp(current_hp, 0, hp);
        StatusUI.isChange = true;
    }
    
    public void SetDamage(int value)
    {
        if (value == -1)
        {
            damage = atk;
        }
        else
            damage = damage * (value / 100);
    }
    public void SetEXP(int value)
    {
        if (value + current_exp >= total_Exp)//�������� ��
        {
            current_exp += value - total_Exp;
            total_Exp += 5000;
            Level++;
            atk += 10;
            def += 10;
            total_atk += 10;
            total_def += 10;
            hp += 100;
            current_hp = hp;
        }
        else
        {
            current_exp += value;
        }
        StatusUI.isChange = true;
    }
    #endregion
}
