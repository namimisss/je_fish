using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AClockworkBerry;

public class ScreenLoggerHotkeyManager : MonoBehaviour
{
    public ScreenLogger screenLogger;
    public bool isloggerOn;
    // Start is called before the first frame update
    void Start()
    {
        screenLogger.ShowLog = isloggerOn;
        if (ScreenLogger.IsPersistent)
            DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F12))
        {
            SwitchLoggerOnAndOff();
        }
    }

    void SwitchLoggerOnAndOff()
    {
        isloggerOn = !isloggerOn;
        screenLogger.ShowLog = isloggerOn;
    }
}
