using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject gm;
	// Use this for initialization
	private GameObject fallenPlayer;
	private float smooth;
	private float size;
	private GameManager script;

	void Start () {
		smooth = 2.0F;
		size = Camera.main.orthographicSize;
		script = gm.GetComponent<GameManager>();

	}

	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = size;

		if(script.isFallen){
			fallenPlayer = script.fallenPlayer;	
			//Camera.main.orthographicSize = Mathf.Lerp (size, 100.0F,Time.time);
			Camera.main.orthographicSize = 400;
			transform.position =  Vector3.Lerp(transform.position,fallenPlayer.transform.position,Time.deltaTime*smooth );


		}
	}
}
