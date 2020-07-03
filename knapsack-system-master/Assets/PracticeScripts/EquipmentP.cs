using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentP:Props
{
    public int Intelligence { get; set; }
    //装备类型的构造函数;
    public EquipmentP(int intelligence, int id, string name, PropsType propsType, PropRarity propRarity, string description, int capacity, string sprite)
        : base(id, name, propsType, propRarity, description, capacity, sprite)
    {
        Intelligence = intelligence;
    }
}
