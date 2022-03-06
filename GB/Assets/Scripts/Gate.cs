
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    //attached to Gate
    public int gateNo;

    public TMP_Text label_fwd;
    public TMP_Text label_rev;

    public void ChangeFwdLabel(string gatename)
    {
        label_fwd.text = gatename;
    }

    public void ChangeRevLabel(string gatename)
    {
        label_rev.text = gatename;
    }


}
