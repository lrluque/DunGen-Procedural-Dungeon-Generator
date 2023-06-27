public class ParameterChangeCommand : iCommand
{
    private DungeonGenerator dungeonGenerator;
    private int width;
    private int height;

    public ParameterChangeCommand(DungeonGenerator generator, int width, int height)
    {
        dungeonGenerator = generator;
        this.width = width;
        this.height = height;
    }

    public void Execute()
    {
        dungeonGenerator.size.x = width;
        dungeonGenerator.size.y = height;
    }
}