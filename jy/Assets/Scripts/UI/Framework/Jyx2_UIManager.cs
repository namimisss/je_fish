using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired.Integration.UnityUI;
using System.Linq;


public enum UILayer
{
    MainUI = 0,//主界面层
    NormalUI = 1,//普通界面层
    PopupUI = 2,//弹出层
    Top = 3,//top层 高于弹出层
}

public class Jyx2_UIManager : MonoBehaviour
{
    static Jyx2_UIManager _instace;

    public static Jyx2_UIManager Instance
    {
        get 
        { 
            if (_instace == null)
            {
                var rewiredObj = FindObjectOfType<Rewired.InputManager>();
                if (rewiredObj == null)
                {
                    var inputMgrPrefab = Resources.Load<GameObject>("RewiredInputManager");
                    var go = Instantiate(inputMgrPrefab);
                    rewiredObj = go.GetComponent<Rewired.InputManager>();
                }

                var canvasPrefab = Resources.Load<GameObject>("MainCanvas");
                var canvsGo = Instantiate(canvasPrefab);
                canvsGo.name = "MainCanvas";
                _instace = canvsGo.GetComponent<Jyx2_UIManager>();
                _instace.Init();

                var rewiredInputModule = canvsGo.GetComponentInChildren<RewiredStandaloneInputModule>();
                rewiredInputModule.RewiredInputManager = rewiredObj;

                DontDestroyOnLoad(_instace);
            }

            return _instace;
        }
    }

    public static void Clear()
    {
        if (_instace == null) return;
        Destroy(_instace.gameObject);
        _instace = null;
    }

    private Transform m_mainParent;
    private Transform m_normalParent;
    private Transform m_popParent;
    private Transform m_topParent;

    private Dictionary<string, Jyx2_UIBase> m_uiDic = new Dictionary<string, Jyx2_UIBase>();
    private Jyx2_UIBase m_currentMainUI;
    private List<Jyx2_UIBase> m_NormalUIs = new List<Jyx2_UIBase>();
    private List<Jyx2_UIBase> m_PopUIs = new List<Jyx2_UIBase>();

    void Init()
    {
        m_mainParent = transform.Find("MainUI");
        m_normalParent = transform.Find("NormalUI");
        m_popParent = transform.Find("PopupUI");
        m_topParent = transform.Find("Top");
    }

    public bool IsTopVisibleUI(Jyx2_UIBase ui)
    {
        if (!ui.gameObject.activeSelf)
            return false;

        if (ui.Layer == UILayer.MainUI)
        {
            //make sure no normal and popup ui on top
            return noShowingNormalUi() &&
                (noInterferingPopupUI());
        }
        else if (ui.Layer == UILayer.NormalUI)
        {
            Jyx2_UIBase currentUi = m_NormalUIs.LastOrDefault();
            if (currentUi == null)
                return true;

            return (ui == currentUi || ui.transform.IsChildOf(currentUi.transform)) && noInterferingPopupUI();
        }
        else if (ui.Layer == UILayer.PopupUI)
        {
            return m_PopUIs.LastOrDefault() == ui;
        }
        else if (ui.Layer == UILayer.Top)
        {
            return true;
        }

        return false;
    }

    private bool noShowingNormalUi()
    {
        return !m_NormalUIs
            .Any(ui => ui.gameObject.activeSelf);
    }

    private bool noInterferingPopupUI()
    {
        //common tips panel has no interaction, doesn't count towards active uis
                return !m_NormalUIs
                    .Any(ui => ui.gameObject.activeSelf) || (m_PopUIs.All(p => p is CommonTipsUIPanel));
    }

}
