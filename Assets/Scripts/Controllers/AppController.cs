using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{

    void Start()
    {
        GetComponent<Generator>().SetWidth(10);
        GetComponent<Generator>().SetHeight(10);
        iCommand GenerateCommand = new GenerateCommand(GetComponent<Generator>());
        GenerateCommand.Execute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
