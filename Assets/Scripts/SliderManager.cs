using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class SliderManager : MonoBehaviour
{

    public Slider sliderWidth, sliderHeight;

    public void ChangeWidth(int width)
    {
        iCommand changeWidthCommand = new ChangeWidthCommand(GetComponent<DungeonGenerator>(), sliderWidth);
        changeWidthCommand.Execute();
    }

    public void ChangeHeight(int height)
    {
        iCommand changeHeightCommand = new ChangeHeightCommand(GetComponent<DungeonGenerator>(), sliderHeight);
        changeHeightCommand.Execute();
    }
}
