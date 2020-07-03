using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责管理下面每一个格子
/// 查找空箱子
/// 存储物品
/// </summary>
public class InventoryP :MonoBehaviour
{
    SlotP[] slotList;
    private void Start()
    {
        //得到物品栏下面的所有的物品信息;
        slotList = GetComponentsInChildren<SlotP>();
    }
    /// <summary>
    /// 寻找存储量为1的物品槽
    /// </summary>
    /// <returns></returns>
    public SlotP FindOneCapacitySlot()
    {
        foreach (var slot in slotList)
        {
            if (slot.transform.childCount==0)
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>
    /// 找到存储相同物品栏且没有存满的格子;
    /// </summary>
    /// <param name="prop"></param>
    /// <returns></returns>
    public SlotP FindBigCapacitySlot(Props prop)
    {
        if (slotList.Length != 0)
        {
            foreach (var slot in slotList)
            {
                //slot下面有物体
                if (slot.transform.childCount >= 1&& slot.GetPropID() == prop.Id && slot.GetSlotAmount < prop.Capacity)
                {  
                    return slot;
                }
            }
        }
        else
        {
            Debug.Log("slotList的容量为0");
        }
        return null;
    }

    /// <summary>
    /// 根据ID得到物品的信息然后存储
    /// 
    /// </summary>
    /// <param name="id"></param>
    public void StoreProp(int id)
    {
        Props prop = InventoryManagerP.Instance.GetPropsByID(id);
        SlotP slot;
        if (prop!=null)
        {
            //这个物品该箱子只能放一个
            if (prop.Capacity==1)
            {
                slot = FindOneCapacitySlot();
                if (slot!=null)
                {
                    slot.StoreProps(prop);
                }
                else
                {
                    Debug.Log("没找到容量为1的空的物品箱");
                }
            }
            //这个物品可以装很多个
            else
            {
                slot = FindBigCapacitySlot(prop);//先找存放相同的物品箱的但是没装满的
           
                if (slot!=null)
                {
                    slot.StoreProps(prop);
                }
                //没找到相同但是没装满的物品箱，找空的物品箱
                else
                {
                    slot = FindOneCapacitySlot();
                    if (slot!=null)
                    {
                        slot.StoreProps(prop);
                    }
                    else
                    {
                        Debug.Log("没箱子了");
                    }
                }
            }
        }
        else
        {
            Debug.Log("prop为空,根据id没找到道具");
        }
    }

}
