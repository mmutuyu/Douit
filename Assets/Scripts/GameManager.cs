using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [Serializable]
    public class Count
    {
        public float minimum;             //Minimum value for our Count class.
        public float maximum;             //Maximum value for our Count class.

        //Assignment constructor.
        public Count(float min, float max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public GameObject[] players;
    public static string[] SCORE_STR = { "Blue", "Red" };

    private static bool pause = false;
    public static int WIN_SCORE = 1;

    //basic set up
    public static GameManager instance = null;
    public GameObject[] PickUps;
    public GameObject OutterShockWave;
    public GameObject InnerShockWave;

    private static float pi = (float)Math.PI;

    //generate item
    private float radius;
    private static float[] ItemProbability = { 0f, 0.85f, 0.95f };
    private Count spawnInterval = new Count(4f, 5f);
    private List<ItemController> itemsOnBoard;
    private int MaxItemsOnBoard = 3;
    private float nextSpawnTime;
    private Vector3 boardCenter;
    private Vector3 boardSize;

    private static GameObject fallingPlayer;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

    }


    // Use this for initialization
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        boardCenter = sr.bounds.center;
        boardSize = sr.bounds.size;
        radius = boardSize.x / 3;        

        nextSpawnTime = Random.Range(spawnInterval.minimum, spawnInterval.maximum);
        itemsOnBoard = new List<ItemController>();
    }

    public void addItemToList(ItemController item)
    {
        itemsOnBoard.Add(item);
    }

    public void removeItemFromList(ItemController item)
    {
        itemsOnBoard.Remove(item);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPaused())
        {
            return;
        }
        //generate items if not reach maximum
        if (itemsOnBoard.Count < MaxItemsOnBoard)
        {
            nextSpawnTime -= Time.deltaTime;
            if (nextSpawnTime <= 0)
            {
                float x = 0, y = 0, r = 0, colliderRadius = 0;
                GameObject toInstantiate = null;
                Vector3 instantiatePosition = new Vector3();
                CircleCollider2D BackGroundCollider = GetComponent<CircleCollider2D>();
                bool isValidPosition = false;
                while (toInstantiate == null || !isValidPosition)
                {
                    nextSpawnTime = Random.Range(spawnInterval.minimum, spawnInterval.maximum);
                    float theta = Random.Range(0f, 2f * pi);
                    r = (float)Math.Sqrt(Random.Range(0f, 1f)) * radius;
                    x = r * (float)Math.Cos(theta);
                    y = r * (float)Math.Sin(theta);
                    float p = Random.Range(0f, 1f);
                    for (int i = PickUps.Length - 1; i >= 0; i--)
                    {
                        if (p >= ItemProbability[i])
                        {
                            toInstantiate = PickUps[i];
                            break;
                        }
                    }
                    colliderRadius = toInstantiate.transform.localScale.x * toInstantiate.GetComponent<CircleCollider2D>().radius;
                    instantiatePosition = boardCenter + new Vector3(x, y, 0);

                    Collider2D[] hits = Physics2D.OverlapCircleAll(instantiatePosition, colliderRadius);
                    isValidPosition = hits.Length == 1 && hits[0] == BackGroundCollider;
                }
                Instantiate(toInstantiate, instantiatePosition, Quaternion.identity);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //player off stage
        if (!pause && (other.tag == "Blue" || other.tag == "Red"))
        {
            Debug.Log("Judge");
            fallingPlayer = other.gameObject;
            //focusCamera(fallingPlayer);
            PlayerController script = other.GetComponent<PlayerController>();
            script.triggerPlayerFall();
            //yield return StartCoroutine(PauseGameForSeconds(5f));
        }
    }

    public void focusCamera(GameObject player)
    {
        Camera.main.transform.localPosition = player.transform.localPosition + new Vector3(0, 0, -100);
        Camera.main.fieldOfView += 1000;
    }

    public void JudgeGame()
    {
        fallingPlayer.SetActive(false);
        String winner = fallingPlayer.tag == "Blue" ? "Red" : "Blue";
        int score = PlayerPrefs.GetInt(winner) + 1;
        if (score >= WIN_SCORE)
        {
            PlayerPrefs.SetInt("Blue", 0);
            PlayerPrefs.SetInt("Red", 0);
            PlayerPrefs.SetString("Winner", winner);
            SceneManager.LoadScene("Win");
        }
        else {
            PlayerPrefs.SetInt(winner, score);
            SceneManager.LoadScene("Main");
        }
    }

    public void startNewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void PauseGame()
    {
        pause = true;
    }

    public void ResumeGame()
    {
        pause = false;
    }

    public IEnumerator PauseGameForSeconds(float pauseTime)
    {
        PauseGame();
        yield return new WaitForSeconds(pauseTime);
        ResumeGame();
    }

    public bool IsPaused()
    {
        return pause;
    }
    
}
