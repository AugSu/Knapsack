using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消耗品
/// </summary>
public class Consumable :Item
{
    public int HP { get; set; }
    public int MP { get; set; }

    public Consumable(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int price, int sellPrice,int hp,int mp,string sprite):
                base(id,name,itemType,quality,description,capacity,price,sellPrice,sprite)
    {
        HP = hp;
        MP = mp;
    }

    public override string GetToolTipText()
    {
        string text = base.GetToolTipText();
        string newText = string.Format("{0}\n\n<color=yellow>加血:{1}\n加蓝:{2}</color>", text, HP, MP);
        return newText;
    }
}
