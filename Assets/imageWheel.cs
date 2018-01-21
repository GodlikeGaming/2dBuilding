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

	public Texture2D rockTile;
	public Sprite s_rockTile;
	public Texture2D grass;
	public Sprite s_grass;

	private int edgesIndex = 0;
	private int[] edges;


	public Material m_rock;
	public Material m_grass;

	public struct polyImage
	{
		public polyImage(int _sides, Texture2D _sprite, GameObject _obj, Material _mat)
		{
			sides = _sides;
			obj = _obj;
			sprite = _sprite;
			Sprite temp = Sprite.Create(_sprite, obj.GetComponent<Image>().sprite.rect, obj.GetComponent<Image>().sprite.pivot);
			obj.GetComponent<Image>().sprite = temp;
			mat = _mat;
		}
		int sides;
		public Texture sprite;
		public GameObject obj;
		public Material mat;

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
			pImages.AddFirst(new polyImage (edges[i], (edges[i] > 5 ? grass : rockTile), temp, (edges[i] > 5 ? m_grass : m_rock)));
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
		pImages.AddLast(new polyImage (edges[edgesIndex], (edges[edgesIndex] > 5 ? grass : rockTile), temp, (edges[edgesIndex] > 5 ? m_grass : m_rock)));
		edgesIndex++;
		return pImages.First.Value;
	}
}
