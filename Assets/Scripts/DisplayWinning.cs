using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplayWinning : MonoBehaviour {

	public Text editor = null;
	void Start()
	{
		editor.text = MyValue.str;
	}
}
