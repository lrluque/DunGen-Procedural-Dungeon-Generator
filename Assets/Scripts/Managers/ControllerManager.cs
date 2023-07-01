using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour, iObserver
{
    
    [SerializeField] private Dictionary<string, Controller> _controllers;
    [SerializeField] private Subject _inputManager;
    private string _activeController;

    void Start()
    {
        _inputManager.AddObserver(this);
        _controllers = new Dictionary<string, Controller>();
        foreach (Controller controller in GetComponentsInChildren<Controller>())
        {
            _controllers.Add(controller.name, controller);
        }
        _activeController = "PlayController";
        Cursor.lockState = CursorLockMode.Confined;
        SetActive("GenerationController");
    }

    public void OnNotify(UserAction action)
    {
        switch(action)
        {
            case UserAction.PLAY:
                SetActive("PlayController");
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case UserAction.RETURN:
                Cursor.lockState = CursorLockMode.Confined;
                _controllers["PlayController"].transform.Find("FirstPersonController").transform.position = new Vector3(0, 1, 0);
                SetActive("GenerationController");
                break;
            default:
                break;
        }
    }

    public void SetActive(string controllerName)
    {
        _controllers[_activeController].SetActive(false);
        _controllers[controllerName].SetActive(true);
        _activeController = controllerName;
    }


}