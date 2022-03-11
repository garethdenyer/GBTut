using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamMove : MonoBehaviour
{
    //attached to MiniMap camera 

    public Camera mainCamera;  //the cameraicon

    float lawnwidth;
    float minimapwidth;

    bool minicamactive;

    private void Start()
    {
        lawnwidth = 15f;
        minimapwidth = 900f;
    }

    private void Update()
    {
        if (minicamactive)
        {
            mainCamera.transform.position = 
                new Vector3(transform.localPosition.y * lawnwidth / minimapwidth, 0.7f, -transform.localPosition.x * lawnwidth / minimapwidth);
        }
        else
        {
            transform.localPosition =
                new Vector2(-mainCamera.transform.position.z * minimapwidth / lawnwidth, mainCamera.transform.position.x * minimapwidth / lawnwidth);
        }
    }

    public void ActivateMiniCam()
    {
        minicamactive = true;
    }

    public void InactivateMiniCam()
    {
        minicamactive = false;
    }

}
