using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseClick : MonoBehaviour {

	// Use this for initialization

	public GameObject polygon;
	public GameObject mouseClickPoint;
	private List<Vector2> points;
	private List<GameObject> mouseClickPoints;
	public PhysicsMaterial2D physMaterial;
	public Text edgeAmountText;

	private int amountOfEdgesAllowed;
	private int edgesIndex = 0;
	private int[] edges;
	void Start () {

		edges = new int[25];
		for (int i = 0; i < 25; i++) {
				edges[i] = Random.Range(3, 8);
		}
		points = new List<Vector2> ();
		mouseClickPoints = new List<GameObject> ();
		amountOfEdgesAllowed = edges [edgesIndex];
		FindObjectOfType<imageWheel> ().Pop ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector2 mousePosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

			points.Add (mousePosition);
			GameObject temp = Instantiate (mouseClickPoint, mousePosition, Quaternion.identity, GameObject.FindGameObjectWithTag ("Canvas").transform) as GameObject;
			mouseClickPoints.Add (temp);
		}	

		//if (Input.GetMouseButtonDown (1) && points.Count > 2) 
		if (amountOfEdgesAllowed == points.Count)
		{
			FindObjectOfType<imageWheel> ().Pop ();
			imageWheel.polyImage pImage = FindObjectOfType<imageWheel> ().Push ();
			EndFigure (pImage.mat);
			points.Clear ();
			foreach (GameObject obj in mouseClickPoints) {
				Destroy (obj);
			}
			mouseClickPoints.Clear ();
			amountOfEdgesAllowed = edges[edgesIndex];

			edgesIndex++;
		}
		edgeAmountText.text = points.Count + " of " + amountOfEdgesAllowed;
	}

	void EndFigure (Material mat)
	{
		Vector2 center = GetCenterPoint ();
		Vector2[] ps = points.ToArray ();
		for (int i = 0; i < ps.Length; i++)
		{
			ps[i] -= center;
		}
		float area = Area (ps);
		//Debug.Log (area);
		GameObject temp = Instantiate (polygon, center, Quaternion.identity) as GameObject;
		temp.GetComponent<createShape> ().myMaterial = mat;
		temp.GetComponent<createShape> ().shapePoints = ps;
		if (!mat.name.Equals ("rock")) {
			temp.GetComponent<createShape> ().withRigidBody = true;
		}
		temp.GetComponent<createShape> ().rbMass = area;
		temp.GetComponent<createShape> ().rbMaterial = physMaterial;
		temp.GetComponent<createShape> ().Initialize ();
	}

	Vector2 GetCenterPoint()
	{
		if (points.Count == 1) {
			return points.ToArray()[0];
		}

		var bounds = new Bounds (points.ToArray()[0], Vector3.zero);
		for (int i = 0; i < points.Count; i++) {
			bounds.Encapsulate (points.ToArray() [i]);
		}

		return bounds.center;
	}

	public float Area (Vector2[] nodePositions) {
		Vector3 result = Vector2.zero;
		for(int p = nodePositions.Length-1, q = 0; q < nodePositions.Length; p = q++) {
			result += Vector3.Cross(nodePositions[q], nodePositions[p]);
		}
		result *= 0.5f;
		return result.magnitude;
	}
}
