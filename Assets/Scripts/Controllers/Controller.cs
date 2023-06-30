using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [SerializeField] private GameObject _controllerObject;

    public void SetActive(bool active)
    {
        _controllerObject.SetActive(active);
    }
}
