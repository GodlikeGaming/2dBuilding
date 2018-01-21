using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageWheel : MonoBehaviour {

	public Vector2 offset = new Vector2(25, 0f);
	public float startOffset;
	private Transform[] images;
	public float distance = 100;
	public GameObject Image;

	//polyImage[] polyImages;
	LinkedList<polyImage> pImages;

	public Texture rockTile;
	public Sprite s_rockTile;
	public Texture grass;
	public Sprite s_grass;

	private int edgesIndex = 0;
	private int[] edges;
	public struct polyImage
	{
		public polyImage(int _sides, Texture _sprite, GameObject _obj)
		{
			sides = _sides;
			obj = _obj;
			sprite = _sprite;

			//obj.GetComponent<Image>().sprite = 
		}
		int sides;
		public Texture sprite;
		public GameObject obj;

	}
	// Use this for initialization
	void Awake () 
	{
		edges = new int[25];
		for (int i = 0; i < 25; i++) {
			edges[i] = Random.Range(3, 8);
		}

		pImages = new LinkedList<polyImage> ();
		for (int i = 0; i < 8; i++) {
			GameObject temp = Instantiate (Image, new Vector2 (0, 0), Quaternion.identity, transform) as GameObject;
			pImages.AddFirst(new polyImage (edges[i], (edges[i] > 5 ? grass : rockTile), temp));
			edgesIndex++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		int count = 0;
		foreach (polyImage pImg in pImages) {
			pImg.obj.transform.position = new Vector2 (count * (distance + offset.x) + startOffset, offset.y);
			count++;
		}
	}

	public void Pop()
	{
			polyImage temp = pImages.First.Value;
			pImages.RemoveFirst ();
			Destroy (temp.obj);
	}
	public polyImage Push()
	{
		GameObject temp = Instantiate (Image, new Vector2 (0, 0), Quaternion.identity, transform) as GameObject;
		pImages.AddLast(new polyImage (edges[edgesIndex], (edges[edgesIndex] > 5 ? grass : rockTile), temp));
		edgesIndex++;
		return pImages.First.Value;
	}
}
