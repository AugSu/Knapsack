using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///物品栏
///设置物品栏的图片;
/// </summary>
public class ItemUIP : MonoBehaviour
{
    //存储的物品信息根据传进来的值将该变量赋值;
    private Props prop;
    private int amount;
    //物品的数量信息;
    private Text text;
    private float targetLocalScale;
    public Props Prop
    {
        get
        {
            return prop;
        }
    }
   
    public int Amount
    {
        get
        {
            return amount;
        }
    }

    private void Start()
    {
        targetLocalScale = 1.0f;
    }

    private void Update()
    {
        if (transform.localScale.x!=targetLocalScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetLocalScale, 2f * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x-targetLocalScale)<0.1f)
            {
                scale = targetLocalScale;
            }
        }
    }

    private Image image;
    public Image ItemUIImage
    {
        get
        {
            if (image==null)
            {
                image = GetComponent<Image>();
            }
            return image;
        }
    }
    /// <summary>
    /// 返回text
    /// </summary>
    public Text ItemUIText
    {
        get
        {
            if (text==null)
            {
                text = GetComponentInChildren<Text>();
            }
            return text;
        }
    }
 
    /// <summary>
    /// 设置物品栏的图片
    /// </summary>
    /// <param name="image"></param>
    public void SetItem(Props prop,int amount=1)
    {
        this.prop = prop;
        ItemUIImage.sprite = Resources.Load<Sprite>(prop.PropSprite);
        //改变ItemUI的尺寸;
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        this.amount = amount;
        //设置个数个数为1 不显示个数；
        if (this.amount == 1)
        {
            ItemUIText.text = "";
        }
       
    }

    public void AddAmount(int number =1)
    {
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        amount += number;
        //数量大于1显示出来;
        ItemUIText.text = amount.ToString();
    }
    /// <summary>
    /// 设置数量
    /// </summary>
    /// <param name="amount"></param>
    public void SetAmount(int amount)
    {
        transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        this.amount = amount;
        //设置个数个数为1 不显示个数；
        if (this.amount == 1)
        {
            ItemUIText.text = "";
        }
        else
        {
            ItemUIText.text = Amount.ToString();
        }
    }

    /// <summary>
    /// 显示PropUI面板
    /// </summary>
    public void  ShowPropUI()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 隐藏PropUI面板;
    /// </summary>
    public void HidePropUI()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector2 targetPosition)
    {
        transform.localPosition = targetPosition;
    }
}
