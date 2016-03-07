using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarBar1 : MonoBehaviour {

	[SerializeField]
	private float fillAmount;
	[SerializeField]
	private Image content;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		HandleBar ();
	}

	public void HandleBar(){
		float starCount1 = GameManager.count2;
		content.fillAmount = Map (starCount1, 5);
	}

	public float Map(float count1, float max){
		return count1 / max;
	}
}
