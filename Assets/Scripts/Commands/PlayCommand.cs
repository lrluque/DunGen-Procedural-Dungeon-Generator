using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCommand : iCommand
{
    private GameObject _generatorController;
    private GameObject _playerController;

    public PlayCommand(GameObject generatorController, GameObject playerController)
    {
        _generatorController = generatorController;
        _playerController = playerController;
    }

    public void Execute()
    {
        _generatorController.SetActive(false);
        _playerController.SetActive(true);
    }

}
