public class DestroyCommand : iCommand
{
    public Generator generator;

    public DestroyCommand(Generator generator)
    {
        this.generator = generator;
    }

    public void Execute()
    {
        generator.Destroy();
    }
}
