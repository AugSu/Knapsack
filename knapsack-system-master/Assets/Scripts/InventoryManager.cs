using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region 单例设计模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion

    private List<Item> itemList;
    //提示信息板子
    private ToolTip toolTip;

    private bool isToolTipShow;
    private Canvas canvas;

    private ItemUI pickedItem;//鼠标选中的物体
    public bool isPickedItem = false;
    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }

    private void Start()
    {
        isToolTipShow = false;
        ParseItemJson();
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();
        
    }
    /// <summary>
    /// 解析物品的信息
    /// </summary>
    void ParseItemJson()
    {
        itemList = new List<Item>();
        //Resource加载text资源
        TextAsset itemText = Resources.Load<TextAsset>("Items");

        //将text转为json支持的jsonReader的类型
        string itemJson = itemText.text;
        JsonReader itemReader = new JsonReader(itemJson);

        //获取json的对象
        JsonData jsonData = JsonMapper.ToObject(itemReader);

        //遍历json对象
        foreach (JsonData temp in jsonData)
        {
            string typeStr = temp["type"].ToString();
            //将string 类转为object类，再转为type类型
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            int id = int.Parse(temp["id"].ToString());
            string name = temp["name"].ToString();
            Item.Quality quality = (Item.Quality)System.Enum.Parse(typeof(Item.Quality), temp["quality"].ToString());
            string description = temp["description"].ToString();
            int capacity = int.Parse(temp["capacity"].ToString());
            int price = int.Parse(temp["price"].ToString());
            int sellPrice = int.Parse(temp["sellPrice"].ToString());
            string sprite = temp["sprite"].ToString();

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = int.Parse(temp["hp"].ToString());
                    int mp = int.Parse(temp["mp"].ToString());
                    item = new Consumable(id, name, type, quality, description, capacity, price, sellPrice,hp,mp,sprite);
                    break;
                case Item.ItemType.Equipment:
                    int strength = int.Parse(temp["strength"].ToString());
                    int intelligence = int.Parse(temp["intelligence"].ToString());
                    int agility = int.Parse(temp["agility"].ToString());
                    int stamina = int.Parse(temp["stamina"].ToString());
                    Equipment.EquipmentType equipType = (Equipment.EquipmentType)System.Enum.Parse(typeof(Equipment.EquipmentType), temp["equipmentType"].ToString());
                    item = new Equipment(id, name, type, quality, description, capacity, price, sellPrice, strength, intelligence, agility, stamina, equipType,sprite);
                    break;
                case Item.ItemType.Weapon:
                    int damage = int.Parse(temp["damage"].ToString());
                    Weapon.WeaponType weaponType = (Weapon.WeaponType)System.Enum.Parse(typeof(Weapon.WeaponType), temp["weaponType"].ToString());
                    item = new Weapon(id, name,type, quality, description, capacity, price, sellPrice, damage, weaponType, sprite);
                    break;
                case Item.ItemType.Material:
                    item = new Material(id, name, type, quality, description, capacity, price, sellPrice, sprite);
                    break;
                default:
                    break;
            }
            itemList.Add(item);
        }
    }

    //提供一个根据ID查找物品的方法

    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID==id)
            {
                return item;
            }
        }

        return null;
    }
    
     
    private void Update()
    {
        //物品的信息板显示
        if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocalPosition(position);
        }

        //鼠标点击物品跟随鼠标移动
        if (isPickedItem)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            PickedItem.SetLocalPosition(position-new Vector2(5,5));
        }

        //物品丢弃处理
        if (isPickedItem&&Input.GetMouseButtonDown(0)&&!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    /// <summary>
    /// 显示ToolTip
    /// </summary>
    /// <param name="content"></param>
    public void ShowToolTip(string content)
    {
        isToolTipShow = true;
        toolTip.Show(content);
    }

    /// <summary>
    /// 隐藏信息板
    /// </summary>
    /// <param name="content"></param>
    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();
    }
    /// <summary>
    /// 点击鼠标捡起物品槽指定的物品
    /// </summary>
    /// <param name="itemUI"></param>
    public void PickedupItem(Item item,int amount)
    {
        pickedItem.SetItem(item,amount);
        PickedItem.Show();
        isPickedItem = true;

        this.toolTip.Hide();//鼠标点击物品的时候信息板隐藏;

    }
    /// <summary>
    /// 从鼠标上拿走一个物品
    /// </summary>
    public void RemoveItem()
    {
        PickedItem.ReduceAmount();
        if (pickedItem.Amount<=0)
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
    }


    /// <summary>
    /// 移出多个
    /// </summary>
    /// <param name="amount"></param>
    public void RemoveItem(int amount=1)
    {
        pickedItem.ReduceAmount(amount);
        if (pickedItem.Amount <= 0)
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
    }
}
