using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeSort : MonoBehaviour
{    
    void gnomeSort(List<GoapAction> list)
    {
        int i = 0;

        while (i < list.Count)
        {
            if (i == 0)
                i++;
            if (list[i - 1].cost >= list[i].cost)
                i++;
            else
            {
                swapVar(list[i], list[i - 1]);
                i--;
            }
        }
    }

    // swaps two values
    void swapVar(GoapAction a, GoapAction b)
    {
        GoapAction temp;
        temp = a;
        a = b;
        b = temp;
    }
}
