using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
    public iCommand command;
    public abstract void init();
    public void Press()
    {
        command.Execute();
    }
}
