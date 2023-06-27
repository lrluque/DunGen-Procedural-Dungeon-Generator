using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SliderScript : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI text;
    public iCommand command;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) => {
            text.text = v.ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
