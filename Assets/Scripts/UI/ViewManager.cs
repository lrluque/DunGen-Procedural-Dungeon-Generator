using System.Collections.Generic;

using UnityEngine;

public class ViewManager : MonoBehaviour, iObserver
{
	[SerializeField] private Dictionary<string, View> _views;
    [SerializeField] private Subject _inputManager;
    private string _activeView;

    void Start()
    {
        _inputManager.AddObserver(this);
        _views = new Dictionary<string, View>();
        foreach (View view in GetComponentsInChildren<View>())
        {
            _views.Add(view.name, view);
        }
        _activeView = "Play";
        SetActive("Generation");
    }


    public void OnNotify(UserAction action)
    {
        switch(action)
        {
            case UserAction.PLAY:
                SetActive("Play");
                break;
            case UserAction.RETURN:
                SetActive("Generation");
                break;
            default:
                break;
        }
    }

    public void SetActive(string viewName)
    {
        _views[_activeView].Hide();
        _views[viewName].Show();
        _activeView = viewName;
    }

}