using UnityEngine.UI;
public class GenerateMazeCommand : iCommand
{
    public DungeonGenerator dungeonGenerator;
    public Slider sliderWidth, sliderHeight;

    public GenerateMazeCommand(DungeonGenerator generator)
    {
        dungeonGenerator = generator;
    }

    public void Execute()
    {
        dungeonGenerator.restartMaze();
    }
}
