using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.up / 10 ;
	}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
