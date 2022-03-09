using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityLog : MonoBehaviour
{
    // Attached to Empty Controller

    public TMP_Text logitem_prefab; //line item to go in content holder
    public GameObject logcontentholder; //content holder in scrollable space

    Setup setupscript;

    private void Start()
    {
        setupscript = this.GetComponent<Setup>();
    }

    public void MakeActivityLogItem(string description, string buttonname, int ballno)
    {
        TMP_Text shotlistitem = Instantiate(logitem_prefab);
        shotlistitem.transform.SetParent(logcontentholder.transform);
        shotlistitem.text =  description;

        if (buttonname== "Touch")
        {
            shotlistitem.GetComponent<ActivityLogItem>().touchbutton.SetActive(true);
            shotlistitem.GetComponent<ActivityLogItem>().touchbuttontext.text = "Touch " + setupscript.balls[ballno].transform.name;
            shotlistitem.GetComponent<ActivityLogItem>().balltouse = ballno;
        }

        if (buttonname == "Score" || buttonname == "Pole")
        {
            shotlistitem.GetComponent<ActivityLogItem>().scorebutton.SetActive(true);
            shotlistitem.GetComponent<ActivityLogItem>().scorebuttontext.text = "Score " + setupscript.balls[ballno].transform.name;
            shotlistitem.GetComponent<ActivityLogItem>().balltouse = ballno;
        }

        if (buttonname == "Outball")
        {
            shotlistitem.GetComponent<ActivityLogItem>().outballbutton.SetActive(true);
            shotlistitem.GetComponent<ActivityLogItem>().outballbuttontext.text = setupscript.balls[ballno].transform.name + " Out";
            shotlistitem.GetComponent<ActivityLogItem>().balltouse = ballno;
        }

    }

}
