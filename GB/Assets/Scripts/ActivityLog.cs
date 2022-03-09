using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ActivityLog : MonoBehaviour
{
    // Attached to Empty Controller

    public TMP_Text logitem_prefab; //line item to go in content holder
    public GameObject logcontentholder; //content holder in scrollable space

    public void MakeActivityLogItem(string description)
    {
        TMP_Text shotlistitem = Instantiate(logitem_prefab);
        shotlistitem.transform.SetParent(logcontentholder.transform);
        shotlistitem.text =  description;
    }

}
