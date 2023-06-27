using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour, iButton
{
    public GameObject generatorController;
    public GameObject playerController;
    public GameObject UI;

    public void Press() {
        generatorController.SetActive(false);
        UI.SetActive(false);
        playerController.SetActive(true);
    }
}
