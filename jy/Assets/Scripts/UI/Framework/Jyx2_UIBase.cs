using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public interface IUIAnimator
{
    void DoShowAnimator();
    void DoHideAnimator();
}

public abstract class Jyx2_UIBase : MonoBehaviour
{
    public virtual UILayer Layer { get; } = UILayer.NormalUI;
    public virtual bool IsOnly { get; } = false;
    public virtual bool IsBlockControl { get; set; } = false;
    public virtual bool AlwaysDisplay { get; } = false;

    protected virtual void OnCreate()
    {

    }

    protected virtual void OnShowPanel(params object[] allParams) { }
    protected virtual void OnHidePanel() { }

    public void Init()
    {
        var rt = GetComponent<RectTransform>();
        rt.localPosition = Vector3.zero;
        rt.localScale = Vector3.one;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        OnCreate();
    }

    public void Show(params object[] allParams)
    {
        this.gameObject.SetActive(true);
        this.transform.SetAsLastSibling();
        this.OnShowPanel(allParams);

        if (this is IUIAnimator) 
        { 
            (this as IUIAnimator).DoShowAnimator();
        }
    }

    public void Hide()
    {
        if (AlwaysDisplay)
        {
            return;
        }

        this.gameObject.SetActive(false);
        this.OnHidePanel();
    }

    public virtual void BindListener(Button button, UnityAction callback, bool supportGamepadButtonsNav = true)
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(callback);
        }
    }

    public virtual void Update()
    {

    }

    protected bool isOnTap()
    {
        return Jyx2_UIManager.Instance.IsTopVisibleUI(this);
    }
}
