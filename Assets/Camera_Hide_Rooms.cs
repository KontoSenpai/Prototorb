using UnityEngine;
using System.Collections;

public class Camera_Hide_Rooms : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {

        RaycastHit hit;
        float distance;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if(Physics.Raycast(transform.position,(forward),out hit))
        {
            print(hit.transform.gameObject.name);
            if( hit.transform.gameObject.name.Contains("Cube"))
            {
                hit.transform.gameObject.active = false;
            }
        }
    }
}
