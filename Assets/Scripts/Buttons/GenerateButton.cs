using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GenerateButton : MonoBehaviour, iButton
{
    public GameObject manager;
    public Slider sliderWidth, sliderHeight;

    public void Press() {
        Generator generator = manager.GetComponent<Generator>();
        iCommand DestroyCommand = new DestroyCommand(generator);
        DestroyCommand.Execute();
        iCommand changeWidthCommand = new ChangeWidthCommand(generator, sliderWidth);
        changeWidthCommand.Execute();
        iCommand changeHeightCommand = new ChangeHeightCommand(generator, sliderHeight);
        changeHeightCommand.Execute();
        iCommand GenerateCommand = new GenerateCommand(generator);
        GenerateCommand.Execute();
    }
}


