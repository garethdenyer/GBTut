using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActivityLogItem : MonoBehaviour
{
    // Attached to ActivityLog item

    public GameObject touchbutton;
    public TMP_Text touchbuttontext;

    public GameObject outballbutton;
    public TMP_Text outballbuttontext;

    public GameObject scorebutton;
    public TMP_Text scorebuttontext;


    StrokeManager strokemanagerscript;
    Minimap minimapscript;
    Scoreboard scoreboardscript;
    Setup setupscript;

    public int balltouse;  //the ball to act on

    private void Start()
    {
        strokemanagerscript = FindObjectOfType<StrokeManager>();
        minimapscript = FindObjectOfType<Minimap>();
        scoreboardscript = FindObjectOfType<Scoreboard>();
        setupscript = FindObjectOfType<Setup>();
    }

    public void SetUpSpark()
    {
        strokemanagerscript.SetUpSpark(balltouse);
    }

    public void BallBecomesOutball()
    {
        
        minimapscript.graphicballs[balltouse].GetComponent<Image>().color = Color.grey;
        setupscript.balls[balltouse].GetComponent<Ball>().SetToBoundary();
    }

    public void ScorePoints()
    {
        scoreboardscript.ChangeScore(balltouse);
    }
}
