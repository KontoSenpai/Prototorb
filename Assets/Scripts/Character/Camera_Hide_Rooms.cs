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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Vector3 camera_Position = transform.position;
        camera_Position.x -= 1;
        if(Physics.Raycast(camera_Position,(forward),out hit))
        {
            if( hit.transform.gameObject.name.Contains("Obstruction"))
                Destroy(hit.transform.gameObject);
        }
    }
}
