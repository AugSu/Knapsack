using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    /// <summary>
    /// 力量
    /// </summary>
    public int Strength { get; set; }
    /// <summary>
    /// 智力
    /// </summary>
    public int Intelligence { get; set; }
    /// <summary>
    /// 敏捷
    /// </summary>
    public int Agility { get; set; }
    /// <summary>
    /// 体力
    /// </summary>
    public int Stamina { get; set; }
    /// <summary>
    /// 装备类型
    /// </summary>
    public EquipmentType equipment { get; set; }

    public Equipment(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int price, int sellPrice,int strength,int intelligence,int agility,int stamina,EquipmentType equipmentType,string sprite):
        base(id,name,itemType,quality,description,capacity,price,sellPrice,sprite)
    {
        Strength = strength;
        Intelligence = intelligence;
        Agility = agility;
        Stamina = stamina;
        equipment = equipmentType;

    }

    public enum EquipmentType
    {
        Head,
        Neck,
        Chest,
        Ring,
        Leg,
        Bracer,
        Boots,
        Trinket,
        Shoulder,
        Belt,
        OffHand
    }


    
    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();

        string equiptypeText = "";
        switch (equipment)
        {
            case EquipmentType.Head:
                equiptypeText += "头部";
                break;
            case EquipmentType.Neck:
                equiptypeText += "项链";
                break;
            case EquipmentType.Chest:
                equiptypeText += "胸甲";
                break;
            case EquipmentType.Ring:
                equiptypeText += "戒指";
                break;
            case EquipmentType.Leg:
                equiptypeText += "腿部";
                break;
            case EquipmentType.Bracer:
                equiptypeText += "护腕";
                break;
            case EquipmentType.Boots:
                equiptypeText += "靴子";
                break;
            case EquipmentType.Trinket:
                equiptypeText += "";
                break;
            case EquipmentType.Shoulder:
                equiptypeText += "护肩";
                break;
            case EquipmentType.Belt:
                equiptypeText += "腰带";
                break;
            case EquipmentType.OffHand:
                equiptypeText += "副手";
                break;
        }
        string newText = string.Format("{0}\n\n<color=yellow>装备类型:{1}\n敏捷:{2}\n智力:{3}\n体力{4}\n力量:{5}</color>", text,equiptypeText,Stamina,Intelligence,Agility,Strength);
        return newText;
    }
}
