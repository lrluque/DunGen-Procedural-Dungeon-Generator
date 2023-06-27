public class GenerateCommand : iCommand
{
    public Generator generator;

    public GenerateCommand(Generator generator)
    {
        this.generator = generator;
    }

    public void Execute()
    {
        generator.Generate();
        generator.Build();
    }
}
