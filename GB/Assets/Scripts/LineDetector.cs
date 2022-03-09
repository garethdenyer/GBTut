using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetector : MonoBehaviour
{
    // Attached to Line

    ActivityLog activityscript;

    private void Start()
    {
        activityscript = FindObjectOfType<ActivityLog>();
    }

    private void OnTriggerEnter(Collider other)
    {
        activityscript.MakeActivityLogItem(other.transform.name + " crossed " + transform.name, "Outball", other.GetComponent<Ball>().ballno);

        Vector3 putinat = other.transform.position;

        switch (transform.name)
        {
            case "Line1":
                putinat += new Vector3(0f, 0f, -0.1f);
                break;

            case "Line4":
                putinat += new Vector3(-0.1f, 0f, 0f);
                break;

            case "Line3":
                putinat += new Vector3(0f, 0f, 0.1f);
                break;

            case "Line2":
                putinat += new Vector3(0.1f, 0f, 0f);
                break;
        }

        other.transform.gameObject.GetComponent<Ball>().lastoffat = putinat;

    }
}
