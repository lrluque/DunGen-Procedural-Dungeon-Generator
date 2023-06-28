using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCommand : iCommand
{
    private GameObject _generatorController, _playerController, _generationUI, _playUI;

    public PlayCommand(GameObject generatorController, GameObject playerController, GameObject generationUI, GameObject playUI)
    {
        _generatorController = generatorController;
        _playerController = playerController;
        _generationUI = generationUI;
        _playUI = playUI;
    }

    public void Execute()
    {
        _generatorController.SetActive(!_generatorController.activeSelf);
        _playerController.SetActive(!_playerController.activeSelf);
        _generationUI.SetActive(!_generationUI.activeSelf);
        _playUI.SetActive(!_playUI.activeSelf);
    }

}
