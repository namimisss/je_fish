using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class GameStart : MonoBehaviour
{
    public CanvasGroup introPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartAsync().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async UniTask StartAsync()
    {
        introPanel.gameObject.SetActive(true);

        introPanel.alpha = 0;
       // await introPanel.DOFade(1, 1f).SetEase(Ease.Linear);
 //       await UniTask.Delay(TimeSpan.FromSeconds(1f));
/*        await introPanel.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(introPanel.gameObject);
        });
*/
        Application.logMessageReceived += OnErrorMsg;

//        ModPanelNew.SwitchSceneTo();
    }

    private void OnErrorMsg(string condition, string stackTrace, LogType logType)
    {
        if (logType == LogType.Exception)
        {
            UnityEngine.Debug.LogWarningFormat("Exception版本:{0}, 触发时间:{1}", Application.version, DateTime.Now);
        }
        else if (logType == LogType.Error)
        {
            UnityEngine.Debug.LogWarningFormat("Error版本:{0}, 触发时间:{1}", Application.version, DateTime.Now);
        }
    }
}
