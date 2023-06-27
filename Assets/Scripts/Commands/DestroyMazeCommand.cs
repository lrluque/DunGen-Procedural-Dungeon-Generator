using UnityEngine.UI;
public class DestroyMazeCommand : iCommand
{
    public DungeonGenerator dungeonGenerator;

    public DestroyMazeCommand(DungeonGenerator generator)
    {
        dungeonGenerator = generator;
    }

    public void Execute()
    {
        dungeonGenerator.DestroyMaze();
    }
}
