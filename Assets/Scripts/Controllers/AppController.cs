using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppController : MonoBehaviour
{

    public Generator generator;
    public Slider sliderWidth, sliderHeight;
    public GameObject generatorController, playerController, UI, app;
    
    private iCommand _command;


    void Start()
    {
        generator = this.GetComponent<Generator>();
        Generate();
    }

    public void Generate()
    {
        _command = new GenerateCommand(generator: generator, sliderWidth: sliderWidth, sliderHeight: sliderHeight);
        _command.Execute();
    }

    public void Play(){
        _command = new PlayCommand(generatorController: generatorController, playerController: playerController);
        _command.Execute();
    }
    


}
