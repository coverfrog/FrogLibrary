using System;

public class SimpleCommand : ICommand
{
    private Action _action;
    
    public SimpleCommand(Action action)
    {
        _action = action;
    }
    
    public void Execute()
    {
        _action?.Invoke();
    }
}