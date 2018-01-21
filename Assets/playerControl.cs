using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	private Rigidbody2D rb;
	public float velocity = 2f;
	public float targetRotation = 0;
	private float yVelocity = 0.0F;
	public float smooth = 0.3F;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.D)) {
			rb.AddForce (velocity * Time.deltaTime * Vector2.right);
		}
		else if (Input.GetKey (KeyCode.A)) {
			rb.AddForce (velocity * Time.deltaTime * -Vector2.right);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			rb.AddForce (velocity * Time.deltaTime * 30 * Vector2.up);
			float yAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, targetRotation, ref yVelocity, smooth);
			Vector3 v = transform.rotation.eulerAngles;
			transform.rotation = Quaternion.Euler (v.x, v.y, yAngle);
		}
	}
}
