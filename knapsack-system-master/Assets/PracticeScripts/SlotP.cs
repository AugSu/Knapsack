using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 负责每一个格子
/// </summary>
public class SlotP : MonoBehaviour,IPointerDownHandler
{
    //挂载在每一个格子下面的物品的UI
    public ItemUIP ItemUI { get; set; }
    //ItemUI组件;
    public GameObject propPrefab;
    private float localScaleValue;

    //格子当前的物品的数量
    public int GetSlotAmount
    {
        get
        {
            return transform.GetChild(0).GetComponent<ItemUIP>().Amount;
        }
    }

    public int GetPropID()
    {
        return transform.GetChild(0).GetComponent<ItemUIP>().Prop.Id;
    }

    /// <summary>
    /// 存储物品信息;
    /// </summary>
    /// <param name="prop"></param>
    public void StoreProps(Props prop)
    {
        //如果物品栏下面没有物品
        if (transform.childCount==0)
        {
            //先将物品挂载在物品栏下面;
            GameObject propGameObject = Instantiate(propPrefab) as GameObject;
            propGameObject.transform.SetParent(this.transform,false);
            propGameObject.transform.localPosition = Vector3.zero;
            //设置物品栏下的物品
            propGameObject.GetComponent<ItemUIP>().SetItem(prop);
        }
        //如果有物品直接存
        else
        {
            //直接将数量+1;
            transform.GetChild(0).GetComponent<ItemUIP>().AddAmount();
        }
    }



    /// <summary>
    /// 鼠标点击事件的处理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        ItemUIP propUI;
        //格子下面有物品
        if (transform.childCount>=1)
        {
            //鼠标下面没有物品
            if (InventoryManagerP.Instance.ispicked == false)
            {
                //得到鼠标点击下的格子的Prop
                propUI = transform.GetChild(0).GetComponent<ItemUIP>();
                InventoryManagerP.Instance.SetPickedItem(propUI);
                //销毁当前格子的PropUI
                Destroy(propUI.gameObject);
            }
        }
        //格子下面没有物品
        else
        {
            //鼠标下面有物品
            if (InventoryManagerP.Instance.ispicked)
            {
                ItemUIP pickedUpProp = InventoryManagerP.Instance.PickUpProp;
                for (int i = 0; i < pickedUpProp.Amount; i++)
                {
                    StoreProps(pickedUpProp.Prop);

                }

                InventoryManagerP.Instance.RemovePickItem();
                
            }
        }
    }
}
