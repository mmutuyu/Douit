using UnityEngine;
using System.Collections;

public class GenericWindow : MonoBehaviour
{

    public StartWindow startWindow;
    public GuideWindow guideWindow;
    public EndWindow endWindow;
    public PauseWindow pauseWindow;

    public static bool newgame = true;
    public static bool endgame = false;

    protected void Display(bool value)
    {
        gameObject.SetActive(value);
    }


    public void Open()
    {
        Display(true);
    }

    public void Close()
    {
        Display(false);
    }

    public void CloseAll()
    {
        startWindow.Close();
        guideWindow.Close();
        pauseWindow.Close();
        endWindow.Close();
    }

    public void ReturnToGame()
    {
        GameManager.instance.ResumeGame();
        CloseAll();
    }

    public void Start()
    {
        if (!newgame) CloseAll();

    }
}
