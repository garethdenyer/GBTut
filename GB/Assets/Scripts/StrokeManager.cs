using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrokeManager : MonoBehaviour
{
    //attached to EmptyController
    GameObject target;
    public GameObject strikerball;

    bool strike = false;

    public GameObject hitbutton;
    public TMP_Text hitbuttontext;

    List<Rigidbody> rbs = new List<Rigidbody>();
    public bool allstopped = true;

    public GameObject aimline3D;

    public List<Button> ballbuts = new List<Button>(); //manually poplulated with list of striker choice buttons

    //for sparking
    public GameObject touchedball;
    public bool sparkmode;

    public Camera mainCamera;

    public void SetObjects()
    {
        target = GameObject.Find("Target");
        NewStriker(0);

        //catalog all the ball rigidbodies
        foreach (GameObject ball in this.GetComponent<Setup>().balls)
        {
            rbs.Add(ball.GetComponent<Rigidbody>());
        }
    }


    public void NewStriker(int ballchoice)
    {
        //shift to rescue a lost ball or after Agari  Coordinates are bottom right, with offset according to ball number
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            this.GetComponent<Minimap>().graphicballs[ballchoice].transform.localPosition = new Vector3(600f - (10f * ballchoice), -475f, 0f);
        }


        //set the striker ball and strikermini to appropriate index
        strikerball = this.GetComponent<Setup>().balls[ballchoice];
        this.GetComponent<Minimap>().strikermini = this.GetComponent<Minimap>().graphicballs[ballchoice];

        //just in case touchedball is still filled
        touchedball = null;

        //restore ball control to 2D minimap
        this.GetComponent<Minimap>().ballsfromlawn = false;

        //clear all selection buttons to grey
        foreach (Button ballbut in ballbuts)
        {
            ballbut.image.color = Color.grey;
        }

        //highlight the selected button and set aimline colours, etc
        if (ballchoice % 2 == 0)  //even normally white but not as we start list at zero :-)
        {
            ballbuts[ballchoice].image.color = Color.red;
            aimline3D.GetComponent<SpriteRenderer>().color = Color.red;
            this.GetComponent<Minimap>().aimline.GetComponent<Image>().color = Color.red;
            hitbutton.GetComponent<Image>().color = Color.red;
            hitbuttontext.color = Color.white;

            this.GetComponent<Minimap>().graphicballs[ballchoice].GetComponent<Image>().color = Color.red;
        }
        else
        {
            ballbuts[ballchoice].image.color = Color.white;
            aimline3D.GetComponent<SpriteRenderer>().color = Color.white;
            this.GetComponent<Minimap>().aimline.GetComponent<Image>().color = Color.white;
            hitbutton.GetComponent<Image>().color = Color.white;
            hitbuttontext.color = Color.black;

            this.GetComponent<Minimap>().graphicballs[ballchoice].GetComponent<Image>().color = Color.white;
        }
    }

    public void CameraStoT()
    {
        Vector2 targetcoords = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 strikercoords = new Vector2(strikerball.transform.position.x, strikerball.transform.position.z);
        Vector2 direction = (targetcoords - strikercoords).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        mainCamera.transform.position = new Vector3(strikerball.transform.position.x, 0.5f, strikerball.transform.position.z) ;
        mainCamera.transform.rotation = Quaternion.Euler(20f, angle, 0);
    }

    public void LookDown()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, 1f, mainCamera.transform.position.z);
        mainCamera.transform.rotation = Quaternion.Euler(90f, mainCamera.transform.rotation.y, 0f);
    }

    private void Update()
    {
        //Draw the 3D aimline
        Vector3 tsdiff = target.transform.position - strikerball.transform.position;  //coordinate difference between striker and target
        Vector2 targetxzcoords = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 strikerxzcoords = new Vector2(strikerball.transform.position.x, strikerball.transform.position.z);
        float u = Vector2.Distance(targetxzcoords, strikerxzcoords);

        float angle; //angle between striker and target
        if (tsdiff.x < 0)
        {
            angle = (180 / Mathf.PI) * Mathf.Asin(tsdiff.z / u);
        }
        else
        {
            angle = 180 - (180 / Mathf.PI) * Mathf.Asin(tsdiff.z / u);
        }

        float ucentrex = ((Mathf.Cos(((180 - angle) * Mathf.PI / 180)) * u) / 2f) + strikerball.transform.position.x;
        float ucentrez = ((Mathf.Sin(((180 - angle) * Mathf.PI / 180)) * u) / 2f) + strikerball.transform.position.z;
        aimline3D.transform.position = new Vector3(ucentrex, 0.05f, ucentrez);
        float fiddle = 2.56f; //not sure why u needs to be reduced by this factor...
        aimline3D.transform.localScale = new Vector3(u/fiddle, 0.02f, 0.02f);
        aimline3D.transform.rotation = Quaternion.Euler(new Vector3(90f, angle, 0f));


        if (sparkmode)
        {
            Vector3 relvectorposn = (target.transform.position - strikerball.transform.position).normalized; //the difference in vectors
            touchedball.transform.position = strikerball.transform.position + new Vector3(relvectorposn.x * 0.08f, 0f, relvectorposn.z * 0.08f);
        }

    }


    public void HitTheStroke()
    {
        this.GetComponent<Minimap>().ballsfromlawn = true;  //ball control from lawn in order to play shot
        strikerball.GetComponent<Ball>().isoutball = false;
        hitbutton.SetActive(false);
        strike = true;
    }

    private void FixedUpdate()
    {
        if (strike)
        {
            //establish the total distance of the shot
            Vector2 targetposxz = new Vector2(target.transform.position.x, target.transform.position.z);
            Vector2 strikerposxz = new Vector2(strikerball.transform.position.x, strikerball.transform.position.z);
            float TotalDist = Vector2.Distance(targetposxz, strikerposxz);

            //apply the player distance error
            float strikerdisterr = strikerball.GetComponent<Ball>().disterror / 100f;
            float randisterr = Random.Range(1 - strikerdisterr, 1 + strikerdisterr);
            TotalDist *= randisterr;

            //establish the angle of the shot - based on Target/Striker difference
            float anglexzdegs = Mathf.Atan2((target.transform.position.x - strikerball.transform.position.x), (target.transform.position.z - strikerball.transform.position.z)) * Mathf.Rad2Deg;

            //apply the player angle error
            float worstangleeror = strikerball.GetComponent<Ball>().angleerror;
            float randangerror = Random.Range(-worstangleeror, worstangleeror);
            anglexzdegs += randangerror;

            //compute the force x and z values from the total distance and angle
            float anglexzrads = anglexzdegs * Mathf.Deg2Rad;            //need angle in radians for this to work
            float forcex = TotalDist * Mathf.Sin(anglexzrads);
            float forcez = TotalDist * Mathf.Cos(anglexzrads);

            //tweak
            forcex *= (-0.0019f * TotalDist) + 0.4174f;
            forcez *= (-0.0019f * TotalDist) + 0.4174f;

            if (sparkmode)
            {
                touchedball.GetComponent<Rigidbody>().AddForce(new Vector3(forcex, 0, forcez), ForceMode.Impulse);

                strikerball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                sparkmode = false;
                hitbuttontext.text = "Stroke";
                touchedball = null;

            }

            else
            {
                strikerball.GetComponent<Rigidbody>().AddForce(new Vector3(forcex, 0f, forcez), ForceMode.Impulse);
            }


        }

        strike = false;

        StartCoroutine(CheckIfBallsStopped());
    }

    IEnumerator CheckIfBallsStopped()
    {
        allstopped = false;  //assume all the balls are moving

        while (!allstopped)
        {
            allstopped = true;

            foreach (Rigidbody rb in rbs)
            {
                if (!rb.IsSleeping())
                {
                    allstopped = false;
                    yield return null;
                }
            }
        }

        //when all the balls have stopped, do the following things

        //switch input to the 2D minimap rather than the 3D lawn
        this.GetComponent<Minimap>().ballsfromlawn = false;

        //reactivate the hitbutton
        hitbutton.SetActive(true);

    }

    public void StopAllBalls()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.Sleep();
        }

        allstopped = true;
    }

    public void SetUpSpark(int touchedno)
    {
        //enable the touched ball to be teleported in 3D
       this.GetComponent<Minimap>().ballsfromlawn = true;

        //freeze teh strikerball and temporarily disable its collider
        strikerball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        strikerball.GetComponent<Collider>().enabled = false;

        //bring over the touched ball and temporaily disable its collider
        touchedball = this.GetComponent<Setup>().balls[touchedno];
        touchedball.GetComponent<Collider>().enabled = false;
        touchedball.transform.position = strikerball.transform.position + new Vector3(0.8f, 0f, 0.8f);

        //reenable the hit button and prepare for Spark
        hitbuttontext.text = "Spark";
        hitbutton.SetActive(true);
        sparkmode = true;

        strikerball.GetComponent<Collider>().enabled = true;
        touchedball.GetComponent<Collider>().enabled = true;
    }

}
