public abstract class Command
{
    public abstract void Execute();

    public abstract bool _isComplete { get; } 

}
