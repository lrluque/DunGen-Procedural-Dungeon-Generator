using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GenerateCommand : iCommand
{
    private Generator _generator;
    private Slider _sliderWidth, _sliderHeight;

    public GenerateCommand(Generator generator, Slider sliderWidth, Slider sliderHeight)
    {
        this._generator = generator;
        this._sliderWidth = sliderWidth;
        this._sliderHeight = sliderHeight;
    }

    public void Execute()
    {
        _generator.Destroy();
        _generator.SetWidth((int)_sliderWidth.value);
        _generator.SetHeight((int)_sliderHeight.value);
        _generator.Generate();
        _generator.Build();
    }

}
