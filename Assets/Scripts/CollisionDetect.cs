using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollisionDetect : MonoBehaviour
{

    public Text countTxt1;
    public Text countTxt2;
    public Text winLose;

	private float stoptime;
	private float freezetime;


    void Start()
    {
        SetTxt();

    }

//	void Awake()
//	{
//		Pause ();
//	}

	void Update()
	{
		
//		if (Time.realtimeSinceStartup - stoptime >= 3) 
//		{
//			Time.timeScale = 1;
//		}
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
		Application.LoadLevel ("finish");
        //yield return new WaitForSeconds(5);
        //winLose.text = "";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //core collider will cause win/fail detect
            if (other.tag == "Player1")
            {
				GameManager.count1 += 1;
				Application.LoadLevel(Application.loadedLevel);
            }
            if (other.tag == "Player2")
            {
                GameManager.count2 += 1;
				Application.LoadLevel(Application.loadedLevel);
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
    }

//	void Pause()
//	{
//		Time.timeScale = 0;
//		stoptime = Time.realtimeSinceStartup;
//	}

