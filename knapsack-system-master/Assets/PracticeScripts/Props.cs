using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 道具类
/// </summary>
public class Props 
{
    //道具的id根据ID将道具分类
    public int Id { get; set; }
    //道具的名字
    public string Name { get; set; }
    //道具的种类
    public PropsType PropsType { get; set; }
    //道具的稀有程度
    public PropRarity PropRarity { get; set;}
    //道具的具体描述
    public string Description { get; set; }
    //道具的容量,这个道具在格子可以存放的个数
    public int Capacity { get; set; }
    //道具的Sprite图片;
    public string PropSprite { get; set; }

    /// <summary>
    /// 道具基类的构造函数
    /// </summary>
    public Props(int id,string name,PropsType propsType,PropRarity propRarity,string description,int capacity,string propSprite)
    {
        Id = id;
        Name = name;
        PropsType = propsType;
        PropRarity = propRarity;
        Description = description;
        Capacity = capacity;
        PropSprite = propSprite;
    }

    public override string ToString()
    {
        return string.Format("id:{0},name:{1},propsType:{2},propRarity{3}", Id, Name, PropsType, PropRarity);
    }

}

/// <summary>
/// 道具的种类
/// </summary>
public enum PropsType
{
    Weapon,
    Equipment,
    Consumerable
}

/// <summary>
/// 武器的罕见程度
/// </summary>
public enum PropRarity
{
    General,
    Rare,
    Epic
}
