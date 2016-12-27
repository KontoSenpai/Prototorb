using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        transform.position -= transform.forward / 10 ;
	}

    void OnTriggerEnter(Collider collision)
    {
        if( !collision.tag.Equals("Player") && !collision.tag.Equals("Obstruction"))
        Destroy(gameObject);
    }
}
