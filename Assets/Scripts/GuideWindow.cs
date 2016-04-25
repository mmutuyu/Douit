using UnityEngine;
using System.Collections;

public class GuideWindow : GenericWindow {

  public void Back(){
    base.CloseAll();
    base.startWindow.Open();
  }

}