using UnityEngine;
using System.Collections;

public class StartWindow : GenericWindow {

  public bool endgame = false;

  public void Guide(){
    base.CloseAll();
    base.guideWindow.Open();
  }

  public void Start(){
    if(endgame){
      Debug.Log("start");
      endgame = false;
      base.CloseAll();
      endWindow.Open();
    }
  }

}