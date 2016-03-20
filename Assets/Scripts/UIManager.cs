using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject[] EnergyBar;
    public GameObject[] StarBar;
    public Text[] AttackButtonText;

    private static String[] AttackButtonTextList = { "None", "GiantGrowth", "Charge", "Confuse" };

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            PlayerController script = GameManager.instance.players[i].GetComponent<PlayerController>();

            setEnergyBar(EnergyBar[i], 0);

            setStarBar(StarBar[i], 0);

            setAttackButtonText(AttackButtonText[i], script.getSkillLevel());
        }
    }

    // Update is called once per frame
    void Update()
    {
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
