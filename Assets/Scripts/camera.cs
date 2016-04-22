using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
	public GameObject gm;
	// Use this for initialization
	private GameObject fallenPlayer;
	private float smooth;
	private float size;
	void Start () {
		smooth = 0.4F;
		size = Camera.main.orthographicSize;
	
	}

	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = size;
		GameManager script = gm.GetComponent<GameManager>();
		if(script.isFallen){
			fallenPlayer = script.fallenPlayer;	
			Camera.main.orthographicSize = 300;
			transform.position =  Vector3.Lerp(transform.position,fallenPlayer.transform.position,Time.deltaTime*smooth );
		}
	}
}
