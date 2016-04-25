using UnityEngine;
using System.Collections;

public class PauseWindow : GenericWindow {

  public void Resum(){
    GameManager.instance.isPaused = false;
    UIManager.instance.enbleButtons(true);
    Time.timeScale = 1;
    base.CloseAll();
  }
}