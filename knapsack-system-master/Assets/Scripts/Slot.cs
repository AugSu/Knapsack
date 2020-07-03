using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject itemPrefab;
    /// <summary>
    /// 将item放在自身而过的下面
    /// 如果自身下面已经有,amount++
    /// 如果没有根据itemPrefab实例化一个item放在下面
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item)
    {
        //格子下面没有空的时候
        if (transform.childCount==0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            //item就是json里面一个个对象，根据item.capcaity判断能否放入多个；
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }

    /// <summary>
    /// 当前物品槽的物品的存储类型;
    /// 得到挂载在Slot下面的Item的itemUI脚本
    /// 然后获取挂载在slot下面的Item的类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
       
        return transform.GetChild(0).GetComponent<ItemUI>().Item.GetItem;
    }

    /// <summary>
    /// 得到物品的ID
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    /// <summary>
    /// 判断当前的物品槽有没有满
    /// </summary>
    /// <returns></returns>
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;
    }
    /// <summary>
    /// /鼠标移入
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //当前格子下面有物体&&鼠标上面没有物体
        if (transform.childCount>0&&InventoryManager.Instance.isPickedItem==false)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }

    /// <summary>
    /// 鼠标的移出事件的处理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {

        if (transform.childCount>0)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }

    /// <summary>
    /// 处理鼠标点击事件;
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (transform.childCount>0)//格子下面的物体数量大于0
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();//当前格子下物体的数量

            if (InventoryManager.Instance.isPickedItem==false)//鼠标上面没有物体
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下Control键的情况
                {
                    int amountPicked = (currentItem.Amount + 1) >> 1;
                    InventoryManager.Instance.PickedupItem(currentItem.Item, amountPicked);
                    int amountRemained = currentItem.Amount - amountPicked;
                    if (amountRemained<=0)//抽取箱子物体后,箱子物体数量<=0
                    {
                        Destroy(currentItem.gameObject);
                    }
                    else//抽取箱子物体后,箱子物体大于0
                    {
                        currentItem.SetAmount(amountRemained);//设置被拿走宝箱的剩余数量的物品
                    }
                }
                else//没按下control键
                {         
                    InventoryManager.Instance.PickedupItem(currentItem.Item,currentItem.Amount);//相当于将物品槽的信息复制给鼠标
                    Destroy(currentItem.gameObject);
                }
            }
            else//鼠标上面有物体(currentItem 是ItemUI)
            {
                //箱子里的物品与鼠标上的物品是一样的类型
                if (currentItem.Item.ID==InventoryManager.Instance.PickedItem.Item.ID)
                {
                    if (Input.GetKey(KeyCode.LeftControl))//按下control键
                    {
                        if (currentItem.Item.Capacity>currentItem.Amount)//当前格子的数量 > 目前存在的装备的数量,没装满
                        {
                            currentItem.AddAmount();//增加装备数量;
                            InventoryManager.Instance.RemoveItem();//鼠标上的物品的数量减一;
                        }
                        else//盒子装满了
                        {
                            return;
                        }
                    }
                    else //没有按下Control键
                    {
                        if (currentItem.Item.Capacity>currentItem.Amount)//当前物品槽还有容量
                        {
                            int amountReamin = currentItem.Item.Capacity - currentItem.Amount;//当前物品槽剩余的空间;
                            if (amountReamin>=InventoryManager.Instance.PickedItem.Amount)//空余容量>鼠标上个数
                            {
                                //设置当前物品槽的数量=当前槽数量 +  鼠标上的数量
                                currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
                                //移出鼠标上的
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                            }//空余容量小于鼠标上的容量
                            else
                            {
                                currentItem.SetAmount(currentItem.Amount + amountReamin);
                                InventoryManager.Instance.RemoveItem(amountReamin);
                            }
                        }
                        else//当前物品槽没容量
                        {
                            Debug.Log("放不下了");
                        }
                    }
                }
                else//鼠标上的物体与当前物品槽的物品不同
                {
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    //将鼠标上的物品设置到物品槽中
                    currentItem.SetItem(InventoryManager.Instance.PickedItem.Item,InventoryManager.Instance.PickedItem.Amount);//物品槽设置
                    //将物品槽上的物品设置到鼠标中
                    InventoryManager.Instance.PickedItem.SetItem(item, amount);
                }
            }
        }
        else//当前储物格子为空的情况
        {
            if (InventoryManager.Instance.isPickedItem)//当前鼠标有东西
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下control,个空格存东子 GetKey是持续监测,
                {
                    StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.RemoveItem();//鼠标上的数量-1;
                }
                else//没按空格键
                {
                    //王物品槽一个一个存东西
                    for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                    {
                        StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                }
            }
        }
                
    }
}
