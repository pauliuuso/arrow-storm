using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

    public int maxXRotation;
    public int maxYRotation;

	void Start ()
    {
        if(maxXRotation != 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x + Random.Range(-maxXRotation, maxXRotation), transform.localEulerAngles.y, transform.localEulerAngles.z));
        }
        if(maxYRotation != 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Random.Range(-maxYRotation, maxYRotation), transform.localEulerAngles.z));
        }
    }

}
