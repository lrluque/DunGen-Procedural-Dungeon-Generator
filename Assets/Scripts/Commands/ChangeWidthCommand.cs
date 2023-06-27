using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeWidthCommand : iCommand
{
    public DungeonGenerator dungeonGenerator;
    public Slider sliderWidth;

    public ChangeWidthCommand(DungeonGenerator generator, Slider slider)
    {
        dungeonGenerator = generator;
        sliderWidth = slider;
    }


    public void Execute()
    {
        dungeonGenerator.setWidth((int)sliderWidth.value);
    }


}