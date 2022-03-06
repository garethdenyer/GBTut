using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    // Attached to EmptyController

    [SerializeField] private GameObject pole_prefab;
    [SerializeField] private GameObject gate_prefab;
    [SerializeField] private GameObject ball_prefab;
    [SerializeField] private GameObject trgt_prefab;

    Vector3 G1 = new Vector3(-3.5f, 0.068f, -8f);
    Vector3 G2 = new Vector3(5.5f, 0.068f, 2f);
    Vector3 G3 = new Vector3(-5.5f, 0.068f, 0f);

    public List<GameObject> gates = new List<GameObject>();
    public List<GameObject> balls = new List<GameObject>();
    List<Vector3> gateposns = new List<Vector3>();

    [SerializeField] Material[] ballstamps;

    private void Start()
    {
        gateposns.Add(G1);
        gateposns.Add(G2);
        gateposns.Add(G3);
        PutOutGates();
        PutOutPole();
        PutOutBalls();
        PutOutTarget();

        this.GetComponent<StrokeManager>().SetObjects();

    }


    void PutOutPole()
    {
        GameObject pole = Instantiate(pole_prefab, new Vector3(0f, 0.068f, 0f), Quaternion.Euler(new Vector3(0f, 0, 0f)));
        pole.transform.name = "Pole";
    }

    void PutOutTarget()
    {
        GameObject target = Instantiate(trgt_prefab, new Vector3(5f, 0.03f, -3f), Quaternion.Euler(new Vector3(0f, 0, 0f)));
        target.transform.name = "Target";
        this.GetComponent<Minimap>().RealTarget = target;
    }

    void PutOutGates()
    {
        for (int g = 0; g < 3; g++)
        {
            float yrot=0;
            if (g == 0) { yrot = 90f; }

            GameObject gate = Instantiate(gate_prefab, gateposns[g], Quaternion.Euler(new Vector3(0f, yrot, 0f)));
            gate.transform.name = "G" + (g + 1).ToString();
            gate.GetComponent<Gate>().gateNo = g + 1;
            gate.GetComponent<Gate>().ChangeRevLabel((g + 1).ToString());
            gate.GetComponent<Gate>().ChangeFwdLabel((g + 1).ToString());
            gates.Add(gate);
        }
    }

    void PutOutBalls()
    {
        for (int i = 1; i < 11; i++)
        {
            //start at Z of -7 and put each new ball 1/8th of a metre further up
            //the i%2 staggers the X coodinate by either an 1/8th of a metre of not at all
            GameObject ball = Instantiate(ball_prefab, new Vector3(-7.7f - ((i % 2) / 8f), 2f, -7f + (i / 8f)), ball_prefab.transform.rotation);
            balls.Add(ball);

            if (i % 2 == 0)                //even - white
            {

                ball.GetComponent<Renderer>().material.color = Color.white;
                ball.GetComponent<Ball>().ballID = "W" + i.ToString();
                ball.transform.name = "W" + i.ToString();
            }
            else                //odd - red
            {

                ball.GetComponent<Renderer>().material.color = Color.red;
                ball.GetComponent<Ball>().ballID = "R" + i.ToString();
                ball.transform.name = "R" + i.ToString();
            }

            ball.GetComponent<Ball>().ballstamp.GetComponent<Renderer>().material = ballstamps[i - 1]; //apply the relevant material for the number on ball
            ball.GetComponent<Ball>().balllabel.text = i.ToString(); //change the label above the ball
        }

    }

}
