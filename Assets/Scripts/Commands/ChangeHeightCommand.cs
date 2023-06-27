using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeHeightCommand : iCommand
{
    public DungeonGenerator dungeonGenerator;
    public Slider sliderHeight;

    public ChangeHeightCommand(DungeonGenerator generator, Slider slider)
    {
        dungeonGenerator = generator;
        sliderHeight = slider;
    }

    public void Execute()
    {
        dungeonGenerator.setHeight((int)sliderHeight.value);
    }
}