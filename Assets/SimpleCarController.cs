using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour {

	public Transform car;
	public int power;
	public int reverse;
	public Vector2 force;
	private Vector3 trigfunction;
	public float maxSpeed = 200f;
	public bool grounded = false;
	public float rotationPower = 2;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		for (int i = 0;i < transform.childCount; i++)
		{
			grounded = transform.GetChild (i).GetComponent<CircleCollider2D> ().IsTouchingLayers();
			if (grounded) {
				break;
			}
		}
		trigfunction = car.TransformDirection(Vector3.right); 
		force.Set(trigfunction.x,trigfunction.y);

		if (grounded) {
			if (Input.GetKey (KeyCode.W)) {
				transform.GetChild (0).GetComponent<Rigidbody2D> ().rotation += power;
				GetComponent<Rigidbody2D> ().AddForce (trigfunction * power);
			} else if (Input.GetKey (KeyCode.S)) {
				transform.GetChild (0).GetComponent<Rigidbody2D> ().rotation -= power;
				GetComponent<Rigidbody2D> ().AddForce (trigfunction * -reverse);
			}

		} else {
			if (Input.GetKey (KeyCode.W)) {
				transform.GetChild (0).GetComponent<Rigidbody2D> ().rotation += power;
				GetComponent<Rigidbody2D> ().AddTorque (rotationPower);
			} else if (Input.GetKey (KeyCode.S)) {
				transform.GetChild (0).GetComponent<Rigidbody2D> ().rotation -= power;
				GetComponent<Rigidbody2D> ().AddTorque (rotationPower);
			}
		}
		if (Input.GetKey (KeyCode.Space)) {
			float yAngle = 0;
			Vector3 v = transform.rotation.eulerAngles;
			transform.rotation = Quaternion.Euler (v.x, v.y, yAngle);
		
		
		}
	}

	void FixedUpdate ()
	{
		if(transform.GetChild(0).GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed) {
			transform.GetChild(0).GetComponent<Rigidbody2D>().rotation = GetComponent<Rigidbody2D>().rotation * maxSpeed;
		}
	}
}
