using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshCreate : MonoBehaviour {

	private GameObject[] myObject = new GameObject[2];
	private Vector2[] nodePositions;
	public Vector2[] shapePoints;
	public bool withRigidBody = false;
	public Color color;
	public bool rotate = false;
	public float rotationFactor = 1f;
	public float rotation = 0;
	public Material myMaterial;
	public float sortingLayer;
	private PolygonCollider2D polyCol;
	public bool notOnScreen;
	// Use this for initialization

	void Start()
	{
		nodePositions = new Vector2[shapePoints.Length]; 
		for (int i = 0; i < shapePoints.Length; i++) {
			nodePositions [i] = new Vector2 (transform.position.x + shapePoints [i].x, transform.position.y + shapePoints [i].y);
		}
		createGameObject ();

		myMaterial.color = color;
	}
	void Update ()
	{
		for (int i = 0; i < shapePoints.Length; i++) {
			nodePositions [i] = new Vector2 (transform.position.x + shapePoints [i].x, transform.position.y + shapePoints [i].y);
		}
		updatePolygon ();
	}
	void createGameObject()
	{
		for (int x = 0; x < 1; x++) {

			myObject [x] = new GameObject ();
			myObject [x].transform.parent = transform;
			myObject [x].layer = 8;
			myObject [x].name = transform.name;

			Mesh mesh = new Mesh ();

			//Components
			MeshFilter MF = myObject [x].AddComponent<MeshFilter> ();

			if (withRigidBody) 
			{
				myObject [x].AddComponent<Rigidbody2D> ();
			}
			MeshRenderer MR = myObject [x].AddComponent<MeshRenderer> ();
			MR.sortingLayerName = "foreground";
			MR.sortingOrder = 0;
			MR.material.color = color;
			polyCol = myObject[x].AddComponent<PolygonCollider2D>();
		}
	}
	public float Area () {
		if (!this.gameObject.activeSelf || notOnScreen) {
			return 0;
		}
		Vector3 result = Vector3.zero;
		for(int p = nodePositions.Length-1, q = 0; q < nodePositions.Length; p = q++) {
			result += Vector3.Cross(nodePositions[q], nodePositions[p]);
		}
		result *= 0.5f;
		return result.magnitude;
	}

	void updatePolygon()
	{
		for(int x = 0; x < 1; x++)
		{
			/*
			Mesh mesh = myObject [x].GetComponent<Mesh> ();
			mesh = CreateMesh(x);*/

			//Assign materials
			myObject [x].GetComponent<MeshRenderer> ().material = myMaterial;

			//Assign mesh to game object
			CreateMesh(x, myObject [x].GetComponent<MeshFilter> ().mesh);
			polyCol.points = nodePositions;
		}
	}
	Mesh CreateMesh(int num, Mesh mesh)
	{

		if (rotate) 
		{
			for (int i = 0; i < nodePositions.Length; i++) {
				nodePositions [i] = Quaternion.Euler(0, 0, rotation) * shapePoints[i] + transform.position;
			}

			/*rotation += rotationFactor;
			if (rotation >= 360) {
				rotation = 0;
			}*/

		}
		//Vertices
		Vector3[] vertex = new Vector3[nodePositions.Length+2];
		int[] triangles = new int[3 * (vertex.Length - 2)];

		vertex [0] = transform.position;
		for(int i = 0; i < vertex.Length-2; i++)
		{
			vertex [i+1] = nodePositions [i];

			if (i < vertex.Length - 2) 
			{
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}
		vertex [nodePositions.Length + 1] = nodePositions [0];

		//Assign data to mesh

		mesh.vertices = vertex;
		mesh.triangles = triangles;

		//Recalculations
		mesh.RecalculateNormals();

		//Name the mesh
		mesh.name = "MyMesh";

		//Return the mesh
		return mesh;
	}
}
