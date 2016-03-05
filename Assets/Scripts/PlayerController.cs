using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization

    public float degree = (float)Math.PI / 360;
    public static Vector3 EnlargeSize = new Vector3(5, 5, 0);
    public static float ItemDurationTime = 5;


    public float SpeedOriginal = 15000f;
    public float SpeedUp = 25000f;

    private Rigidbody2D rb2d;
    private bool isRotate;
    private float angel;
    private float[] statusTimeLeft;

    private Vector3 ScaleOriginal;
    private Vector3 ScaleEnlarged;
    private float curSpeed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isRotate = true;
        angel = 100;
        statusTimeLeft = new float[2];

        ScaleOriginal = transform.localScale;
        ScaleEnlarged = ScaleOriginal + EnlargeSize;

        curSpeed = SpeedOriginal;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, angel * Time.deltaTime);
        }

        //check for item status
        for (int i = 0; i < statusTimeLeft.Length; i++)
        {
            statusTimeLeft[i] -= Time.deltaTime;

            if (statusTimeLeft[i] > 0 && statusTimeLeft[i] - Time.deltaTime < 0)
            {

                switch (i)
                {
                    case 0:
                        PlayerEnlarge(false);
                        //speed *= SpeedChange;
                        break;
                    case 1:
                        PlayerSpeedUp(false);
                        break;
                }

            }
        }
    }

    void FixedUpdate()
    {
        if (!isRotate)
        {
            rb2d.AddRelativeForce(new Vector2(0, 1) * curSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Item_Enlarge")
        {
            if (statusTimeLeft[0] <= 0)
            {
                PlayerEnlarge(true);
            }
            statusTimeLeft[0] = ItemDurationTime;

            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Item_Speed")
        {
            if (statusTimeLeft[1] <= 0)
            {
                PlayerSpeedUp(true);
            }
            statusTimeLeft[1] = ItemDurationTime;

            other.gameObject.SetActive(false);
        }

    }

    public void ChangeRotationStatus()
    {
        isRotate = !isRotate;
    }

    public void ChangeRotationDirection()
    {
        angel = -angel;
    }

    public void PlayerEnlarge(bool isEnlarged)
    {
        transform.localScale = isEnlarged ? ScaleEnlarged : ScaleOriginal;
    }

    public void PlayerSpeedUp(bool isSpeedUp)
    {
        curSpeed = isSpeedUp ? SpeedUp : SpeedOriginal;
    }


}
