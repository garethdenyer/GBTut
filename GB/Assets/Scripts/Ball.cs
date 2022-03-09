using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public string ballID; //R1 to W10
    public GameObject ballstamp; //the physical mesh that forms the number on the ball 
    public TMP_Text balllabel; //number displayed on the top of the ball when stationary
    public GameObject label;
    public int ballno;

    Rigidbody rb;

    public int score;

    //for player characteristics
    public float angleerror;
    public float disterror;
    public string playername;

    StrokeManager strokemanagerscript;
    ActivityLog activityscript;
    Minimap minimapscript;

    public Vector3 lastoffat;
    public bool isoutball;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        angleerror = 1f;
        disterror = 1f;

        strokemanagerscript = FindObjectOfType<StrokeManager>();
        activityscript = FindObjectOfType<ActivityLog>();
        minimapscript = FindObjectOfType<Minimap>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Gateball" && strokemanagerscript.strikerball.transform.name == transform.name && strokemanagerscript.sparkmode == false)
        {
            activityscript.MakeActivityLogItem(transform.name + " hits " + collision.transform.name, "Touch", collision.transform.gameObject.GetComponent<Ball>().ballno);
        }
    }

    public void SetToBoundary()  //will put the map icon to the point where the ball crossed
    {
        //stop it from moving and place on spot
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        //run the function in maximapscript that translates a lawn position to a map position, putting in lastoffat
        minimapscript.graphicballs[ballno].transform.localPosition = minimapscript.iconcoords(lastoffat.x, lastoffat.z, 60f);

        isoutball = true;
    }

}
