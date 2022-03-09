using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetector : MonoBehaviour
{
    // Attached to Gate detector

    ActivityLog activityscript;

    private void Start()
    {
        activityscript = FindObjectOfType<ActivityLog>();
    }

    private void OnTriggerEnter(Collider other)
    {
        activityscript.MakeActivityLogItem(other.transform.name + " entered " + transform.parent.name);
    }

    private void OnTriggerExit(Collider other)
    {
        activityscript.MakeActivityLogItem(other.transform.name + " exited " + transform.parent.name);
    }
}

