using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleDetector : MonoBehaviour
{
    // Attached to Pole

    ActivityLog activityscript;

    private void Start()
    {
        activityscript = FindObjectOfType<ActivityLog>();
    }

    private void OnCollisionEnter(Collision other)
    {
        activityscript.MakeActivityLogItem(other.transform.name + " hit " + transform.parent.name, "Pole", other.transform.gameObject.GetComponent<Ball>().ballno);
    }
}
