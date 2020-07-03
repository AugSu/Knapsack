using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryManagerP:MonoBehaviour
{
    //鼠标抓起的道具;
    private ItemUIP pickedUpProp;
    public ItemUIP PickUpProp
    {
        get
        {
            return pickedUpProp;
        }
    }
    //鼠标下面是否有物体
    public bool ispicked;
    private Transform canvas;


    private static InventoryManagerP instance;
    public static InventoryManagerP Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("InventoryManager").GetComponent<InventoryManagerP>();
            }
            return instance;
        }
    }

    private void Start()
    {
        ParseJson();
        //获取鼠标下的propUI组件
        pickedUpProp = GameObject.Find("PickedUpProp").GetComponent<ItemUIP>();
        canvas = GameObject.Find("Canvas").transform;
        //该组件隐藏
        pickedUpProp.HidePropUI();
    }

    private void Update()
    {
        PropFollowCursor();
    }

    //存储道具种类列表
    private List<Props> propsList;
    /// <summary>
    /// 解析Json
    /// </summary>
    private void ParseJson()
    {
        propsList = new List<Props>();
        TextAsset ta = Resources.Load<TextAsset>("PropsJson");
        JsonReader jsonReader = new JsonReader(ta.text);
        JsonData jsonObject = JsonMapper.ToObject(jsonReader);
        int id;
        string name;
        PropsType propsType;
        PropRarity propRarity;
        string description;
        int capacity;
        string sprite;
        foreach (JsonData data in jsonObject)
        {
            id = int.Parse(data["id"].ToString());
            name = data["name"].ToString();
            propsType = (PropsType)System.Enum.Parse(typeof(PropsType),data["propsType"].ToString());
            propRarity = (PropRarity)System.Enum.Parse(typeof(PropRarity), data["propRarity"].ToString());
            capacity = int.Parse(data["capacity"].ToString());
            description = data["description"].ToString();
            sprite = data["sprite"].ToString();
            Props prop=null;
            switch (propsType)
            {
                case PropsType.Weapon:
                    int damage = int.Parse(data["damage"].ToString());
                    int increaseSpeed = int.Parse(data["increaseSpeed"].ToString());
                    prop = new WeaponP(damage,increaseSpeed,id, name, propsType, propRarity, description,capacity,sprite);
                    break;
                case PropsType.Equipment:
                    int intelligence = int.Parse(data["intelligence"].ToString());
                    prop = new EquipmentP(intelligence, id, name, propsType, propRarity, description, capacity, sprite);
                    break;
                case PropsType.Consumerable:
                    int hp = int.Parse(data["hp"].ToString());
                    int mp = int.Parse(data["mp"].ToString());
                    prop = new ConsumableP(hp, mp, id, name, propsType, propRarity, description, capacity, sprite);
                    break;
            }
            propsList.Add(prop);
        }
    }

    /// <summary>
    /// 根据ID得到物品信息;
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Props GetPropsByID(int id)
    {
        foreach (var item in propsList)
        {

            if (item.Id==id)
            {
                return item;
            }
        }
        return null;
    }
    /// <summary>
    ///设置鼠标位置下的物品;
    /// </summary>
    /// <param name="prop"></param>
    public void SetPickedItem(ItemUIP itemUIP)
    {
        //设置鼠标下propUI的相关信息
        pickedUpProp.SetItem(itemUIP.Prop);
        pickedUpProp.SetAmount(itemUIP.Amount);
        pickedUpProp.ShowPropUI();
        ispicked = true;
    }

    /// <summary>
    /// 物品跟随鼠标
    /// </summary>
    public void PropFollowCursor()
    {
        if (ispicked)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas as RectTransform, Input.mousePosition, null, out position);
            pickedUpProp.SetLocalPosition(position);
        }
    }
    /// <summary>
    /// 隐藏鼠标上的物品
    /// </summary>
    public void RemovePickItem()
    {
        ispicked = false;
        pickedUpProp.HidePropUI();
    }


}
