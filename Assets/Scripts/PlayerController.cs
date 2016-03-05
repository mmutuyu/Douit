using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Text powerCountText;

    /// <summary>
    /// static parameters
    /// </summary>
    private static float degree = (float)Math.PI / 360;

    //skill
    private static float StatusDurationTime = 5;

    //bonus
    private static float PowerBonus = 1f;
    private static float PowerBonusDecrease = 0.05f;

    //enlarge
    private static Vector3 EnlargeSize = new Vector3(5, 5, 0);
    private static float MassOriginal;
    private static float MassChange = 2f;

    //speedup
    private static float SpeedOriginal = 15000f;
    private static float SpeedUp = 25000f;
    private static float SpeedCharge = SpeedOriginal * 70;
    



    /// <summary>
    /// non-static parameters
    /// </summary>

    private Rigidbody2D rb2d;
    private Vector3 ScaleOriginal;
    private Vector3 ScaleEnlarged;

    //status
    //0:enlarge, 1:speedup, 2:reverse controller
    private float[] statusTimeLeft = { 0, 0, 0 };
    private float powerCount = 0f;
    private float[] PowerLevels = { 1f, 3f, 5f };

    //movement
    private float angel;
    private float curSpeed;
    private bool isRotate;
    private bool isReversed;
    private bool isCharge=false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MassOriginal = rb2d.mass;
        ScaleOriginal = transform.localScale;
        ScaleEnlarged = ScaleOriginal + EnlargeSize;

        angel = 100;
        curSpeed = SpeedOriginal;
        isRotate = true;
        isReversed = false;

        powerCountText.text = powerCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerCount > 0)
        {
            powerCount -= Time.deltaTime * PowerBonusDecrease;
            powerCount = Math.Max(powerCount, 0);
        }
        setText();

        PlayerRotation();

        CheckStatusTime();

    }

    //check each status' left time
    private void CheckStatusTime()
    {
        for (int i = 0; i < statusTimeLeft.Length; i++)
        {
            statusTimeLeft[i] -= Time.deltaTime;

            if (statusTimeLeft[i] > 0 && statusTimeLeft[i] - Time.deltaTime < 0)
            {

                switch (i)
                {
                    case 0:
                        PlayerEnlarge(false);
                        break;
                    case 1:
                        PlayerSpeedUp(false);
                        break;
                    case 2:
                        PlayerControllerReverse(false);
                        break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement(curSpeed);
    }

    //control player rotation
    private void PlayerRotation()
    {
        if (!isCharge && (isRotate != isReversed))
        {
            transform.Rotate(0, 0, angel * Time.deltaTime);
        }
    }

    //control player movement
    private void PlayerMovement(float speed)
    {
        if (isCharge || (isRotate == isReversed))
        {
            rb2d.AddRelativeForce(new Vector2(0, 1) * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "PowerBonus")
        {
            powerCount += PowerBonus;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Trap")
        {
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

    private void PlayerEnlarge(bool isEnlarged)
    {
        if (isEnlarged)
        {
            transform.localScale = ScaleEnlarged;
            rb2d.mass = MassOriginal * MassChange;
        }
        else
        {
            transform.localScale = ScaleOriginal;
            rb2d.mass = MassOriginal;
        }
    }

    private void PlayerSpeedUp(bool isSpeedUp)
    {
        if (isSpeedUp)
        {
            curSpeed = SpeedUp;
        }
        else
        {
            curSpeed = SpeedOriginal;
        }
    }

    private void PlayerCharge()
    {
        isCharge = true;
        PlayerMovement(SpeedCharge);
    }

    private void PlayerControllerReverse(bool isReversed)
    {
        this.isReversed = isReversed;
    }

    public void skillHandler()
    {
        for (int i = PowerLevels.Length - 1; i >= 0; i--)
        {
            float thisLevel = PowerLevels[i];
            if (powerCount >= thisLevel)
            {
                switch (i)
                {
                    case 0:
                        statusTimeLeft[0] = StatusDurationTime;
                        PlayerEnlarge(true);
                        break;
                    case 1:
                        //statusTimeLeft[1] = StatusDurationTime;
                        isCharge = true;
                        PlayerCharge();
                        isCharge = false;
                        break;
                    case 2:
                        statusTimeLeft[2] = StatusDurationTime;
                        PlayerControllerReverse(true);
                        break;
                }
                return;
            }
        }
    }

    private void setText()
    {
        powerCountText.text = powerCount.ToString();
    }

}
