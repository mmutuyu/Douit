using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarBar2 : MonoBehaviour {

	[SerializeField]
	private float fillAmount;
	[SerializeField]
	private Image content;

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
		float starCount2 = GameManager.count1;
		content.fillAmount = Map (starCount2, 5);
	}

	public float Map(float count1, float max){
		return count1 / max;
	}
}
