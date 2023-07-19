using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class AppManager : MonoBehaviour, iObserver
{

    [SerializeField] private Generator _activeGenerator;
    [SerializeField] private Slider _sliderWidth, _sliderHeight, _sliderOffset;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _spawnLocation;
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private Subject _inputManager;

    
    
    private iCommand _command;


    void Start()
    {
        _inputManager.AddObserver(this);
        _activeGenerator = new DepthFirstSearchGenerator(rooms: _rooms, spawnLocation: _spawnLocation);
        Generate();
    }

    public void Generate()
    {
        _command = new GenerateCommand(generator: _activeGenerator, sliderWidth: _sliderWidth, sliderHeight: _sliderHeight);
        _command.Execute();
    }

    void ChangeGenerator()
    {
        if (_dropdown.value == 0)
        {
            _activeGenerator = new DepthFirstSearchGenerator(rooms: _rooms, spawnLocation: _spawnLocation);
        }
        else if (_dropdown.value == 1)
        {
            _activeGenerator = new BSPDungeonGenerator(rooms: _rooms, spawnLocation: _spawnLocation);
        }
    }

    public void Quit(){
        Application.Quit();
    }
    
    void Update()
    {

    }

    public void OnNotify(UserAction action)
    {
        switch (action)
        {
            case UserAction.GENERATE:
                Generate();
                break;
            case UserAction.QUIT:
                Quit();
                break;
            case UserAction.ALGORITHM:
                ChangeGenerator();
                break;
            default:
                break;
            
        }
    }


}
