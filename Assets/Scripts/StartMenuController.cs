using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    void Awake() {
        PlayerPrefs.DeleteAll();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startNewGame()
    {
        SceneManager.LoadScene("Main");
    }
}
