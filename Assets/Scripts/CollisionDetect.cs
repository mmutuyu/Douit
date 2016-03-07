using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{

    public Text countTxt1;
    public Text countTxt2;
    public Text winLose;

    private float stopTime;
    private float freezeTime = 3f;


    void Start()
    {
        SetTxt();

    }

    void Awake()
    {
        //Pause();
    }

    void Update()
    {
        /*
        if (Time.realtimeSinceStartup - stopTime >= freezeTime)
        {
            Time.timeScale = 1;
        }
        */
    }

    void SetTxt()
    {
        countTxt1.text = "Count1: " + GameManager.count1.ToString();
        countTxt2.text = "Count2: " + GameManager.count2.ToString();
        winLose.text = "";
    }

    void SetCountToZero(string whowin)
    {
        GameManager.count1 = 0;
        GameManager.count2 = 0;
        MyValue.str = whowin;
        SceneManager.LoadScene("finish");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //core collider will cause win/fail detect
        if (other.tag == "Player1")
        {
            GameManager.count1 += 1;
            SceneManager.LoadScene("Main");
        }
        if (other.tag == "Player2")
        {
            GameManager.count2 += 1;
            SceneManager.LoadScene("Main");
        }
        SetTxt();
        if (GameManager.count1 >= 5)
        {
            //red win
            SetCountToZero("Red Win!");
        }
        else if (GameManager.count2 >= 5)
        {
            //white win
            SetCountToZero("Blue Win");
        }
    }

    /*
    void Pause()
    {
        Time.timeScale = 0;
        stopTime = Time.realtimeSinceStartup;
    }
    */
}