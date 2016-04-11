using UnityEngine;
using System.Collections;

public class InnerShockWaveController : OutterShockWaveController {
    
	// Update is called once per frame
	void Update () {

        if (GameManager.instance.IsPaused() || !isActiveAndEnabled || timeLeft < 0)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
    }
}
