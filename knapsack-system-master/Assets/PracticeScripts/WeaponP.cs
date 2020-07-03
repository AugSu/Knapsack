using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponP :Props
{
    //武器附加的伤害值
    public int Damage { get; set; }
    //增加的移送速度
    public int IncreaseSpeed { get; set; }
    /// <summary>
    /// 武器的构造函数
    /// </summary>
    public WeaponP(int damage,int increaseSpeed,int id,string name,PropsType propsType,PropRarity propRarity,string description, int capacity,string sprite)
        :base(id,name,propsType,propRarity,description,capacity,sprite)
    {
        Damage = damage;
        IncreaseSpeed = increaseSpeed;
    }
}
