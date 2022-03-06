using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public string ballID; //R1 to W10
    public GameObject ballstamp; //the physical mesh that forms the number on the ball 
    public TMP_Text balllabel; //number displayed on the top of the ball when stationary
    public GameObject label;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //only show the info panel while ball is stationary
        if (rb.velocity.x > 0 || rb.velocity.z > 0)
        {
            label.SetActive(false);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            label.SetActive(true);
        }
    }

}
