using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

	[SerializeField]
	private float fillAmount;
	[SerializeField]
	private Image content;

	public GameObject player;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}
		HandleBar ();
	}

	public void HandleBar(){
		float count = player.GetComponent<PlayerController> ().getPowerCount ();
		content.fillAmount = Map (count, 5);

	}

	public float Map(float count1, float max){
		return count1 / max;
	}
}
