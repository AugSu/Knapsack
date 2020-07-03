using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 16);
            Knapsack.Instance.StoreItem(id);
        }
        //B控制背包的显示和隐藏
        //Knapsack是继承inventory, 再给Knapsack搞个单例模式
        //inventory不能弄单利模式,因为inventory是背包箱子各自拥有一份
        //先继承,再单例模式,
        if (Input.GetKeyDown(KeyCode.B))
        {
            Knapsack.Instance.Switch();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Chest.Instance.Switch();
        }
    }
}
