using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品的基类;
/// </summary>
public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType GetItem { get; set; }
    public Quality GetQuality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int Price { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    public Item()
    {
        this.ID = -1;
    }

    public Item(int id,string name,ItemType itemType,Quality quality,string description,int capacity,int price,int sellPrice,string sprite)
    {
        ID = id;
        Name = name;
        GetItem = itemType;
        GetQuality = quality;
        Description = description;
        Capacity = capacity;
        Price = price;
        SellPrice = sellPrice;
        Sprite = sprite;
    }

    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }

    public enum Quality
    {
        Common,
        UnCommon,
        Rare,
        Epic,
        Legendary,
        Artifical
    }
    
    /// <summary>
    /// 得到ToolTip提示面板应该显示什么样的信息.
    /// </summary>
    /// <returns></returns>
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (GetQuality)
        {
            case Quality.Common:
                color = "white";
                break;
            case Quality.UnCommon:
                color = "lime";
                break;
            case Quality.Rare:
                color = "navy";
                break;
            case Quality.Epic:
                color = "magenta";
                break;
            case Quality.Legendary:
                color = "orange";
                break;
            case Quality.Artifical:
                color = "red";
                break;
        }
        string text = string.Format("<color={4}>{0}</color>\n<color=green>购买价格:{1}出售价格:{2}</color>\n<color=orange>{3}</color>", Name, Price, SellPrice,Description,color);
         
        return text;
    }
}
