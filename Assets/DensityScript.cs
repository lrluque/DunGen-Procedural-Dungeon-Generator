using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DensityScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _slider;

    void Start()
    {
        _dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(_dropdown);});
        _slider.SetActive(false);
    }


    public void DropdownValueChanged(TMP_Dropdown change)
    {
        if (_dropdown.value == 1)
        {
            _slider.SetActive(true);
        }else{
            _slider.SetActive(false);
        }
    }
}
