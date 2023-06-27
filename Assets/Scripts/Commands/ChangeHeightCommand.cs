using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeHeightCommand : iCommand
{
    public Generator generator;
    public Slider sliderHeight;

    public ChangeHeightCommand(Generator generator, Slider slider)
    {
        this.generator = generator;
        sliderHeight = slider;
    }

    public void Execute()
    {
        generator.SetHeight((int)sliderHeight.value);
    }
}