using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour {

	SpriteRenderer sr;
	private float amount = -.025f;
	private float count = .5f;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log (amount + "\t" + count);

		count += amount;


		sr.color = new Color (1f, 1f, 1f, count);
		//sr.color = Color.green;
		if (count <= 0.0f || count >= 1f) {
			amount = amount * -1;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			Debug.Log ("You won!");
		}
		Destroy (this.gameObject);
	}
}
