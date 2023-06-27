using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void Create()
    {
        iCommand GenerateCommand = new GenerateCommand(GetComponent<Generator>());
        GenerateCommand.Execute();
    }

    public void Destroy()
    {
        iCommand DestroyCommand = new DestroyCommand(GetComponent<Generator>());
        DestroyCommand.Execute();
    }
    
}
