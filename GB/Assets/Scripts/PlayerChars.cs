using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerChars : MonoBehaviour
{
    // Attached to Action Controller

    public GameObject playerpanel;
    public TMP_Text paneltitle;

    public Slider angleerror;
    public Slider distanceerror;
    public TMP_InputField playername;
    public TMP_Text angleerrortext;
    public TMP_Text disterrortext;

    GameObject ballobject; //the actual ball in the game

    public void ShowPanel(int ballno)
    {

        playerpanel.SetActive(true);
        paneltitle.text = "Player " + (ballno+1).ToString();
        ballobject = this.GetComponent<Setup>().balls[ballno];

        //set panel colour
        if (ballno % 2 == 0)
        {
            playerpanel.GetComponent<Image>().color = new Color(0.9f, 0.6f, 0.6f, 1f); //a pinky red
        }
        else
        {
            playerpanel.GetComponent<Image>().color = Color.white;
        }

        //display values from BallGB2 in the planel
        angleerror.value = ballobject.GetComponent<Ball>().angleerror;
        distanceerror.value = ballobject.GetComponent<Ball>().disterror;
        playername.text = ballobject.GetComponent<Ball>().playername;

        UpdateDistErrorDescription(distanceerror.value);
        UpdateAngleErrorDescription(angleerror.value);

    }

    public void UpdateDistErrorDescription(float sliderinput)
    {
        disterrortext.text = "Distance Error " + sliderinput.ToString("N1") + " %";
    }

    public void UpdateAngleErrorDescription(float sliderinput)
    {
        angleerrortext.text = "Aim Error " + sliderinput.ToString("N2") + " deg";
    }


    public void SetBallAttributes()  //send values in pane to ball script
    {
        ballobject.GetComponent<Ball>().angleerror = angleerror.value;
        ballobject.GetComponent<Ball>().disterror = distanceerror.value;
        ballobject.GetComponent<Ball>().playername = playername.text;

        playerpanel.SetActive(false);
    }

}
