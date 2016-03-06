using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


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

    public static int count1 = 0;
    public static int count2 = 0;

    //basic set up
    public static GameManager instance = null;
    public GameObject[] PickUps;
    public SpriteRenderer spriteRender;

    private static float pi = (float)Math.PI;

    //generate item
    private float radius;
    private Count spawnInterval = new Count(4f, 5f);
    private List<ItemController> itemsOnBoard;
    private int MaxItemsOnBoard = 3;
    private float nextSpawnTime;
    private Vector3 boardCenter;
    private Vector3 boardSize;

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
        boardCenter = spriteRender.bounds.center;
        boardSize = spriteRender.bounds.size;
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
        //generate items if not reach maximum
        if (itemsOnBoard.Count < MaxItemsOnBoard)
        {
            nextSpawnTime -= Time.deltaTime;
            if (nextSpawnTime <= 0)
            {
                float x = 0, y = 0, r = 0, colliderRadius = 0;
                GameObject toInstantiate = null;
                Vector3 instantiatePosition = new Vector3();
                while (toInstantiate == null || Physics2D.OverlapCircle(instantiatePosition, colliderRadius) != null)
                {
                    nextSpawnTime = Random.Range(spawnInterval.minimum, spawnInterval.maximum);
                    float theta = Random.Range(0f, 2f * pi);
                    r = (float)Math.Sqrt(Random.Range(0f, 1f)) * radius;
                    x = r * (float)Math.Cos(theta);
                    y = r * (float)Math.Sin(theta);
                    toInstantiate = PickUps[Random.Range(0, PickUps.Length)];
                    colliderRadius = toInstantiate.transform.localScale.x * toInstantiate.GetComponent<CircleCollider2D>().radius;
                    instantiatePosition = boardCenter + new Vector3(x, y, 0);
                    if (Physics2D.OverlapCircle(instantiatePosition, colliderRadius) != null)
                    {
                        Collider2D hit = Physics2D.OverlapCircle(instantiatePosition, colliderRadius);
                        Debug.Log("pos:" + instantiatePosition + " and radius:" + colliderRadius + " has Collision:" + hit.gameObject.transform.localPosition);
                    }
                }
                Instantiate(toInstantiate, instantiatePosition, Quaternion.identity);
            }
        }
    }
}
