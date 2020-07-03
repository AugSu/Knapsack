using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }

    private Image itemImage;
    private Text amountText;
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);
    private float targetScale = 0.8f;

    private void Update()
    {
        if (transform.localScale.x!=targetScale)
        {
            float scale= Mathf.Lerp(transform.localScale.x, targetScale,3f*Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x-targetScale)<.02f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    public Image ItemImage
    {
        get
        {
            //Item实例化以后会马上执行SetItem方法,这里不加上判断会itemImage会报空
            //将这些初始化放在Start()方法中也不行,太慢了会报空
            if (itemImage==null)
            {
                itemImage = GetComponentInChildren<Image>();
            }
            return itemImage;
        }
    }

   public Text AmountText
    {
        get
        {
            if (amountText==null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    /// <summary>
    /// 设置每一个格子的UI
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void SetItem(Item item,int amount=1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity>1)
        {
            AmountText.text =Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    /// <summary>
    /// 物品数量增加
    /// </summary>
    /// <param name="amount"></param>
    public void AddAmount(int amount=1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        amountText.text = Amount.ToString();
    }

    /// <summary>
    /// 鼠标点击格子值改变的方法
    /// 设置ItemUI的Amount值
    /// </summary>
    /// <param name="amount"></param>
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    /// <summary>
    /// ItemUI隐藏
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// itemUI显示
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    
    /// <summary>
    /// 鼠标上的物品数目减少
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceAmount(int amount =1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }
}
