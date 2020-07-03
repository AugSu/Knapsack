using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage { get; set; }
    public WeaponType WpType { get; set; }
    public Weapon(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int price, int sellPrice,int damage,WeaponType weaponType,string sprite)
        : base(id,name,itemType,quality,description,capacity,price,sellPrice,sprite)
    {
        Damage = damage;
        WpType = weaponType;
    }

    public enum WeaponType
    {
        OffHand,
        MainHand
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string weapontypeText = "";
        switch (WpType)
        {
            case WeaponType.OffHand:
                weapontypeText += "副手";
                break;
            case WeaponType.MainHand:
                weapontypeText+="主手";
                break;
            default:
                break;
        }
        string newText = string.Format("{0}\n\n<color=red>武器类型:{1}\n攻击力:{2}</color>", text, weapontypeText, Damage);
        return newText;
    }
}
