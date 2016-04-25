using UnityEngine;
using System.Collections;

public class StartWindow : GenericWindow {


  public void Guide(){
    base.CloseAll();
    base.guideWindow.Open();
  }

    public new void Start()
    {
        GameManager.instance.PauseGame();
        base.Start();
    }

}