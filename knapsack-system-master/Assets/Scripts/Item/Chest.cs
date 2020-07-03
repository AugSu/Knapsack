using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventory
{
    private static Chest _instance;
    public static Chest Instance
    {
        get
        {
            if (_instance==null)
            {
                //注意下面的方法不可取,inventory继承自Monobehavior不能通过new()实例化
                //_instance = new Chest();
                _instance = GameObject.Find("Chest").GetComponent<Chest>();
            }
            return _instance;
        }
    }


}
