using UnityEngine;

public class StrokeManager : MonoBehaviour
{
    //attached to EmptyController


    GameObject target;
    public GameObject strikerball;

    bool strike = false;

    public void SetObjects()
    {
        target = GameObject.Find("Target");
        strikerball = this.GetComponent<Setup>().balls[0];
    }

    public void HitTheStroke()
    {
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

            //establish the angle of the shot - based on Target/Striker difference
            float anglexzdegs = Mathf.Atan2((target.transform.position.x - strikerball.transform.position.x), (target.transform.position.z - strikerball.transform.position.z)) * Mathf.Rad2Deg;

            //compute the force x and z values from the total distance and angle
            float anglexzrads = anglexzdegs * Mathf.Deg2Rad;            //need angle in radians for this to work
            float forcex = TotalDist * Mathf.Sin(anglexzrads);
            float forcez = TotalDist * Mathf.Cos(anglexzrads);

            //tweak
            forcex *= (-0.0019f * TotalDist) + 0.4174f;
            forcez *= (-0.0019f * TotalDist) + 0.4174f;

            strikerball.GetComponent<Rigidbody>().AddForce(new Vector3(forcex, 0f, forcez), ForceMode.Impulse);
        }

        strike = false;
    }
}
