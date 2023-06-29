using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppController : MonoBehaviour
{

    public Generator generator;
    public Slider sliderWidth, sliderHeight;
    public GameObject generatorController, playerController, generationUI, playUI, app, spawnLocation;
    public GameObject[] rooms;
    
    
    private iCommand _command;


    void Start()
    {
        generator = new BSPDungeonGenerator(rooms: rooms, spawnLocation: spawnLocation, roomDensity: 10);
        Generate();
    }

    public void Generate()
    {
        _command = new GenerateCommand(generator: generator, sliderWidth: sliderWidth, sliderHeight: sliderHeight);
        _command.Execute();
    }

    public void Play(){
        Cursor.lockState = CursorLockMode.Locked;
        _command = new PlayCommand(generatorController: generatorController, playerController: playerController, generationUI: generationUI, playUI: playUI);
        _command.Execute();
    }

    public void Quit(){
        Application.Quit();
    }
    
    void Update()
    {
        if (playerController.activeSelf){
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.Confined;
                GameObject.Find("FirstPersonController").transform.position = new Vector3(0, 1, 0);
                _command = new PlayCommand(generatorController: generatorController, playerController: playerController, generationUI: generationUI, playUI: playUI);
                _command.Execute();
            }
        }
    }


}
