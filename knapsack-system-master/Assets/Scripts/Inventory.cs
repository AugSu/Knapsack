using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //所有的物品槽
    protected Slot[] slotList;
    private float targetAlpha;
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    //子类可能覆盖
    protected virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        targetAlpha = 1f;
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    private void Update()
    {
        //
        if (canvasGroup.alpha!=targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 2f * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha-targetAlpha)<0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool StoreItem(int id)
    {
        //通过ID获取json文件中的物品
        Item item = InventoryManager.Instance.GetItemByID(id);
        return StoreItem(item);
    }

    public bool StoreItem(Item item)
    {
        if (item==null)
        {
            Debug.Log("要存储的物品的ID不存在");
            return false;
        }
        //该物品的容量为1
        if (item.Capacity == 1)
        {
            //寻找新的空的物品槽
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);
                return true;
            }
        }
        //该物品的(每一格)存储容量大于1
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot!=null)
            {
                slot.StoreItem(item);
            }
            else
            {
                //没有找到类型相同还没存满的箱子
                //在找一个新的空箱子
                Slot emptySlot = FindEmptySlot();
                if (emptySlot!=null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.Log("没有空的物品槽");
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 找到一个空的物品槽
    /// </summary>
    /// <returns></returns>
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount==0)
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>
    /// 只有ID相同表示两个是同一种物品
    /// eg:都是血瓶/蓝瓶
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount>=1&&slot.GetItemId()==item.ID&&slot.IsFilled()==false)
            {
                return slot;
            }
        }
        return null;
    }

    public void Switch()
    {
        if (targetAlpha==0)
        {
            show();
        }
        else
        {
            Hide();
        }
    }

    /// <summary>
    /// 背包的显示和隐藏
    /// </summary>
    private void Hide()
    {
        targetAlpha = 0f;
        //将面板的Raycast禁用掉,禁用掉就不可以交互
        canvasGroup.blocksRaycasts = false;
    }

    private void show()
    {
        targetAlpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


}
