using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followStar : MonoBehaviour {

	public Vector3 offset = new Vector3 (25f, 25f, 25f);
	public float dstFromPlayer = 4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject star = GameObject.FindGameObjectWithTag ("Star");
		/*
		Vector3 direction = Camera.main.WorldToScreenPoint(star.transform.position) - Camera.main.WorldToScreenPoint(player.transform.position);
		direction.Normalize ();
		direction *= (Camera.main.WorldToScreenPoint(star.transform.position) - Camera.main.WorldToScreenPoint(player.transform.position) - offset).magnitude;

		
		//direction.Normalize ();

		Debug.Log (Screen.width);
		direction *= Vector2.Distance(Vector2.zero, new Vector2 (Screen.width/2,Screen.height/2));

*/	
		if (player != null && star != null) {
			Vector3 dir = star.transform.position - player.transform.position;
			transform.position = dir * dstFromPlayer + player.transform.position;
			Vector2 v2 = star.transform.position - player.transform.position;
			float angle = Mathf.Atan2 (v2.y, v2.x) * Mathf.Rad2Deg - 90;
			Debug.Log (angle);
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		}
		/*

		Vector3 direction = Camera.main.WorldToScreenPoint(star.transform.position) - Camera.main.WorldToScreenPoint(player.transform.position);
		Debug.DrawLine (Camera.main.WorldToScreenPoint(player.transform.position), Camera.main.WorldToScreenPoint(star.transform.position), Color.green);
		direction += new Vector3 (Screen.width / 2, Screen.height / 2) - Camera.main.WorldToScreenPoint (player.transform.position);
		Debug.DrawLine (Camera.main.WorldToScreenPoint(player.transform.position), direction);
		transform.position = direction;
		//Debug.Log (transform.position);	*/
	}
}
