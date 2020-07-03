using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 材料类
/// </summary>
public class Material : Item
{
    public Material(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int price, int sellPrice, string sprite)
         : base(id, name, itemType, quality, description, capacity, price, sellPrice, sprite)
    {
    }
}
