using UnityEngine;

public class ItemController : MonoBehaviour
{

    private static float ItemDisappearTime = 10f;
    private static float ItemRotTime = 5f;

    private float timeLeft;

    void Start()
    {
        GameManager.instance.addItemToList(this);
        timeLeft = ItemDisappearTime;
    }

    //Update is called every frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        GameManager.instance.removeItemFromList(this);
    }
}
