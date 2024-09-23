using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    private string stageLevel;

    public string StageLevel
    {
        get { return stageLevel; }

        set { stageLevel = value; }
    }

    public List<string> orders;

    public List<string> Orders
    {
        get { return orders; }

        set { orders = value; }
    }
    
    public Order(string level ,List<string> orderList)
    {
        stageLevel = level;
        orders = orderList;
    }
}