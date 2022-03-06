using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    // Attached to EmptyController

    public List<GameObject> graphicballs = new List<GameObject>();  //populated manually in Inspector with each miniball

    public GameObject RealTarget;
    public GameObject graphicTarget;
    public GameObject strikermini;

    public bool ballsfromlawn = false;
    public Image togglesourceblock;

    public bool viewaimline = false;
    public Image aimline;

    Setup setupscript;

    public RectTransform LawnRect;
    float Reallawnwidth = 15f;

    public Image toggleaimlineblock;



    private void Start()
    {
        setupscript = this.GetComponent<Setup>();
        strikermini = graphicballs[0];
    }

    void Update()
    {
        float panelscaleratio = LawnRect.sizeDelta.y / Reallawnwidth;

        //do the balls
        for (int i = 0; i < 10; i++)
        {
            if (ballsfromlawn)
            {
                graphicballs[i].transform.localPosition =
                    iconcoords(setupscript.balls[i].transform.position.x, setupscript.balls[i].transform.position.z, panelscaleratio);
            }

            else
            {
                TeleportRealItem(setupscript.balls[i], graphicballs[i].transform.localPosition.x, graphicballs[i].transform.localPosition.y);
            }
        }

        //do the target
        if (ballsfromlawn)
        {
            graphicTarget.transform.localPosition = iconcoords(RealTarget.transform.position.x, RealTarget.transform.position.z, panelscaleratio);
        }

        else
        {
            TeleportRealItem(RealTarget, graphicTarget.transform.localPosition.x, graphicTarget.transform.localPosition.y);
        }



        if (viewaimline)
        {
            aimline.GetComponent<Image>().enabled = true;
            //draw the connector line
            RectTransform conlinerect = aimline.GetComponent<RectTransform>();
            Vector2 targetmaxicoords = new Vector2(graphicTarget.transform.localPosition.x, graphicTarget.transform.localPosition.y);
            Vector2 strikermaxicoords = new Vector2(strikermini.transform.localPosition.x, strikermini.transform.localPosition.y);
            Vector2 direction = (targetmaxicoords - strikermaxicoords).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float TSdistance = Vector2.Distance(targetmaxicoords, strikermaxicoords);

            float anglexzrads = angle * Mathf.Deg2Rad;
            float ucentrey = ((Mathf.Sin(anglexzrads) * TSdistance) / 2f) + strikermaxicoords.y;
            float ucentrex = ((Mathf.Cos(anglexzrads) * TSdistance) / 2f) + strikermaxicoords.x;

            conlinerect.sizeDelta = new Vector2(TSdistance, 3); //3 is width of line
            //conlinerect.localPosition = ((targetmaxicoords - strikermaxicoords) / 2f) + strikermaxicoords;
            conlinerect.localPosition = new Vector3(ucentrex, ucentrey, 0f);
            conlinerect.localEulerAngles = new Vector3(0, 0, angle);
        }

        else
        {
            aimline.GetComponent<Image>().enabled = false;
        }

    }

    public Vector3 iconcoords(float realposx, float realposz, float scaleratio)
    {
        float panelposx = realposz * -scaleratio;
        float panelposy = realposx * scaleratio;

        return new Vector3(panelposx, panelposy, 0);
    }

    public void TeleportRealItem(GameObject item, float minimapx, float minimapy)
    {
        float realminiratio = Reallawnwidth / LawnRect.sizeDelta.y;
        if (item == RealTarget)
        {
            item.transform.position = new Vector3(minimapy * realminiratio, 0.0251f, -minimapx * realminiratio);
        }
        else
        {
            item.transform.position = new Vector3(minimapy * realminiratio, 0.0625f, -minimapx * realminiratio);
        }
    }

    public void Toggle2Daimline()
    {
        if (viewaimline)
        {
            viewaimline = false;
            toggleaimlineblock.transform.localPosition += new Vector3(-20f, 0f, 0f);
        }
        else
        {
            viewaimline = true;
            toggleaimlineblock.transform.localPosition += new Vector3(20f, 0f, 0f);
        }
    }

    public void ToggleBallControl()
    {
        if (ballsfromlawn)
        {
            ballsfromlawn = false;
            togglesourceblock.transform.localPosition = new Vector3(-10f, 0f, 0f);
            togglesourceblock.GetComponent<Image>().color = new Color(0.56f, 0.52f, 0.15f, 0.8f);

        }
        else
        {
            ballsfromlawn = true;
            togglesourceblock.transform.localPosition = new Vector3(10f, 0f, 0f);
            togglesourceblock.GetComponent<Image>().color = new Color(0.42f, 0.84f, 0.22f, 0.9f);
        }
    }

}
