using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void CreateMaze()
    {
        iCommand DungeonGeneratorCommand = new GenerateMazeCommand(GetComponent<DungeonGenerator>());
        DungeonGeneratorCommand.Execute();
    }

    public void DestroyMaze()
    {
        iCommand DungeonGeneratorCommand = new DestroyMazeCommand(GetComponent<DungeonGenerator>());
        DungeonGeneratorCommand.Execute();
    }
    
}
