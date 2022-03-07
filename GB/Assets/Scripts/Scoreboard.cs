using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    //Attached to Empty Controller

    public List<TMP_Text> scoresontheboard = new List<TMP_Text>();  //a list of the scores on the board

    public TMP_Text redteamscore;
    public TMP_Text whiteteamscore;

    Setup setupscript;

    private void Start()
    {
        setupscript = this.GetComponent<Setup>();
        redteamscore.text = "0";
        whiteteamscore.text = "0";
    }


    public void ChangeScore(int ballID)
    {
        int oldscore = setupscript.balls[ballID].GetComponent<Ball>().score;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            setupscript.balls[ballID].GetComponent<Ball>().score = ScoreChange(oldscore, "minus");
        }
        else
        {
            setupscript.balls[ballID].GetComponent<Ball>().score = ScoreChange(oldscore, "plus");
        }

        scoresontheboard[ballID].text = setupscript.balls[ballID].GetComponent<Ball>().score.ToString();
        TallyScores();

    }



    int ScoreChange(int incoming, string plusminus)
    {
        int newscore = incoming;

        if (plusminus == "plus" && incoming < 5)
        {
            if (incoming < 3)
            {
                newscore += 1;
            }
            else
            {
                newscore += 2;
            }
        }
        else if (plusminus == "minus" && incoming > 0)
        {
            if (incoming < 4)
            {
                newscore -= 1;
            }
            else
            {
                newscore -= 2;
            }
        }

        return newscore;
    }


    public void TallyScores()
    {
        int redscore = 0;
        int whitescore = 0;

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                //even - red
                redscore += setupscript.balls[i].GetComponent<Ball>().score;
            }
            else
            {
                //odd - white
                whitescore += setupscript.balls[i].GetComponent<Ball>().score;
            }
        }

        redteamscore.text = redscore.ToString();
        whiteteamscore.text = whitescore.ToString();
    }

}
