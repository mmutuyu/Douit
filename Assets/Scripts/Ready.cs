using UnityEngine;
using System.Collections;

public class Ready : MonoBehaviour {

	private float stoptime;
	private float freezetime;

	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		Pause ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup - stoptime >= 1) 
		{
			Time.timeScale = 1;
			gameObject.SetActive (false);
		}
	}


	void Pause()
	{
		Time.timeScale = 0;
		stoptime = Time.realtimeSinceStartup;
	}
}
	