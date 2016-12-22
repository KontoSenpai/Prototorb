using UnityEngine;
using System.Collections;

public class TopDownCharacterShoot : MonoBehaviour {

    public GameObject bullet;

    private const float pistolFireCD = 0.5f;
    private bool canFire = true;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetAxis("Right_Trigger") < - 0.1 && canFire)
        {
            GameObject bull = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            bull.transform.Rotate(new Vector3(90, 0, 0));
            StartCoroutine(fireCD());
        }
	}

    private IEnumerator fireCD()
    {
        canFire = false;
        yield return new WaitForSeconds(pistolFireCD);
        canFire = true;
    }
}
