using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消耗品类
/// </summary>
public class ConsumableP:Props
{
    //消耗品增加血量的属性
    public int HP { get; set; }
    //消耗品增加魔量的属性
    public int MP { get; set; }

    //消耗品的构造函数
    public ConsumableP(int hp,int mp,int id,string name,PropsType type,PropRarity propRarity,string description,int capacity,string sprite)
        :base(id,name,type,propRarity,description,capacity,sprite)
    {
        MP = mp;
        HP = hp;
    }
}
