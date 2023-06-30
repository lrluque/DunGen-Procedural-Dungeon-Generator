using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    [SerializeField] private List<iObserver> _observers = new List<iObserver>();

    public void AddObserver(iObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(iObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void Notify(UserAction action)
    {
        foreach (iObserver observer in _observers)
        {
            observer.OnNotify(action: action);
        }
    }
}
