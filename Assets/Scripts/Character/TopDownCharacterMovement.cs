using UnityEngine;
using System.Collections;

public class TopDownCharacterMovement : MonoBehaviour {

    public GameObject gun_Pivot;
    public GameObject character_Camera;

    private const int max_Velocity = 8;
    private const float analog_Thresold = 0.2f;


    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        // MOVING - Left stick
        Vector3 movement = Vector3.zero;

        movement.z = Input.GetAxis("Left_Analog_Horizontal");
        movement.x = Input.GetAxis("Left_Analog_Vertical");

        if ( Mathf.Abs(movement.z) <analog_Thresold)
            movement.z = 0;
        if ( Mathf.Abs(movement.x) < analog_Thresold)
            movement.x = 0;

        Move(movement);

        // AIMING - Right stick
        float xAim = Input.GetAxis("Right_Analog_Horizontal");
        float yAim = Input.GetAxis("Right_Analog_Vertical");

        if (Mathf.Abs(xAim) < analog_Thresold)
            xAim = 0.0f;
        if (Mathf.Abs(yAim) < analog_Thresold)
            yAim = 0.0f;

        Aim( xAim, yAim);
    }

    private void Move(Vector3 direction)
    {
        if (direction == Vector3.zero)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        else
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


        float vSpeed = -9.8f * Time.deltaTime;
        direction.y = vSpeed;

        GetComponent<CharacterController>().Move( direction / max_Velocity );

        Vector3 relative_Position = transform.position;
        // Position pour la camera
        relative_Position.x += 2;
        relative_Position.y += 2.7f;
        character_Camera.transform.position = relative_Position;
        // Position pour le canon
        relative_Position = transform.position;
        relative_Position.y += 0.25f;
        gun_Pivot.transform.position = relative_Position;
    }

    private void Aim(float x, float y)
    {
        float angle = 0.0f;
        angle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 90;
        if (Mathf.Abs(x) > analog_Thresold || Mathf.Abs(y) > analog_Thresold)
            gun_Pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
