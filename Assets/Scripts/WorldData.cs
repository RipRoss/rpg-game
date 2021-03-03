using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorldData
{
    public bool[] pickupsToShow; // weather or not to show them.

    public WorldData()
    {
        pickupsToShow = PickupItem.instance.pickupsToShow;
    }
}
