using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeWidthCommand : iCommand
{
    public Generator generator;
    public Slider sliderWidth;

    public ChangeWidthCommand(Generator generator, Slider slider)
    {
        this.generator = generator;
        sliderWidth = slider;
    }


    public void Execute()
    {
        generator.SetWidth((int)sliderWidth.value);
    }


}