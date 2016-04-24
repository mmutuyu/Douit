using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject[] EnergyBar;
    public GameObject[] StarBar;
    public GameObject ReadyBar;

    public Button[] AttackButtons;
    public Button[] MoveButtons;

    private static float FREEZE_TIME = 2f;

    private static String[] AttackButtonTextList = { "None", "Charge", "GiantGrowth", "ShockWave" };

    public static UIManager instance = null;

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
    IEnumerator Start()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            PlayerController script = GameManager.instance.players[i].GetComponent<PlayerController>();

            setEnergyBar(EnergyBar[i], 0);

            setStarBar(StarBar[i], 0);

            AttackButtons[i].GetComponentInChildren<Text>().text = AttackButtonTextList[script.getSkillLevel() + 1];
        }

        yield return GameManager.instance.PauseGameForSeconds(FREEZE_TIME);
        ReadyBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused || GameManager.instance.isGameOver)
        {
            return;
        }
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            PlayerController script = GameManager.instance.players[i].GetComponent<PlayerController>();

            setEnergyBar(EnergyBar[i], (float)script.getPowerCount() / PlayerController.POWER_MAX_RANGE);

            setStarBar(StarBar[i], (float)PlayerPrefs.GetInt(GameManager.SCORE_STR[i]) / GameManager.WIN_SCORE);

            AttackButtons[i].GetComponentInChildren<Text>().text = AttackButtonTextList[script.getSkillLevel()+1];
            
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
    
    public void enbleButtons(bool active)
    {
        for (int i = 0; i < AttackButtons.Length; i++)
        {
            AttackButtons[i].interactable = active;
        }
        for (int i = 0; i < MoveButtons.Length; i++)
        {
            MoveButtons[i].interactable = active;
        }
    }

}
