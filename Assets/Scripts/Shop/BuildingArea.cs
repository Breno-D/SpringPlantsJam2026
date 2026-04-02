using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingArea : MonoBehaviour
{
    void OnMouseEnter()
    {
        ShopManager.instance.SetIsInBuildArea(true);
    }

    void OnMouseExit()
    {
        ShopManager.instance.SetIsInBuildArea(false);
    }
}
