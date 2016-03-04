using UnityEngine;

public class ItemController : MonoBehaviour {

    public static float ItemDisappearTime = 30f;

    private float timeLeft;

    void Start() {
        GameManager.instance.addItemToList(this);
        timeLeft = ItemDisappearTime;
    }

	//Update is called every frame
	void Update () 
	{
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {               
            gameObject.SetActive(false);
        }
	}

    void OnDisable() {
        GameManager.instance.removeItemFromList(this);
    }
}
