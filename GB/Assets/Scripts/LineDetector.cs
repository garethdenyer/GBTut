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
        activityscript.MakeActivityLogItem(other.transform.name + " crossed " + transform.name);
    }
}
