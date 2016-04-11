using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject[] EnergyBar;
    public GameObject[] StarBar;
    public Text[] AttackButtonText;
    public GameObject ReadyBar;

    private static float FREEZE_TIME = 2f;

    private static String[] AttackButtonTextList = { "None", "GiantGrowth", "Charge", "ShockWave" };

    // Use this for initialization
    IEnumerator Start()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            PlayerController script = GameManager.instance.players[i].GetComponent<PlayerController>();

            setEnergyBar(EnergyBar[i], 0);

            setStarBar(StarBar[i], 0);

            setAttackButtonText(AttackButtonText[i], script.getSkillLevel());
        }

        yield return StartCoroutine(GameManager.instance.PauseGameForSeconds(FREEZE_TIME));
        ReadyBar.SetActive(false);

    }    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPaused())
        {
            return;
        }
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            PlayerController script = GameManager.instance.players[i].GetComponent<PlayerController>();

            setEnergyBar(EnergyBar[i], (float)script.getPowerCount() / PlayerController.POWER_MAX_RANGE);

            setStarBar(StarBar[i], (float)PlayerPrefs.GetInt(GameManager.SCORE_STR[i]) / GameManager.WIN_SCORE);

            setAttackButtonText(AttackButtonText[i], script.getSkillLevel());
        }
    }

    void setEnergyBar(GameObject energyBar, float amount)
    {
        energyBar.GetComponent<Image>().fillAmount = amount;
    }

    void setStarBar(GameObject starBar, float amount)
    {
        starBar.GetComponent<Image>().fillAmount = amount;
    }

    void setAttackButtonText(Text abt, int skill)
    {
        abt.text = AttackButtonTextList[skill + 1];
    }


}
