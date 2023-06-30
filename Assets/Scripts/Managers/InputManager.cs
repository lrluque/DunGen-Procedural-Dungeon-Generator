using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : Subject
{

    [SerializeField] private Button _playButton, _generateButton, _quitButton;
    [SerializeField] private Slider _sliderWidth, _sliderHeight;
    [SerializeField] private TMP_Dropdown _dropdown;

    [SerializeField] private List<iObserver> _observers = new List<iObserver>();

    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(delegate {Notify(UserAction.PLAY);});
        _generateButton.onClick.AddListener(delegate {Notify(UserAction.GENERATE);});
        _quitButton.onClick.AddListener(delegate {Notify(UserAction.QUIT);});
        _dropdown.onValueChanged.AddListener(delegate {Notify(UserAction.ALGORITHM);});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Notify(UserAction.RETURN);
        }
    }
}
