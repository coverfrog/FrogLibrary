using UnityEngine;

public class MovePositionCommand : ICommand
{
    private readonly MoveHelper _helper;
    private readonly MoveContext _context;
    
    public MovePositionCommand(MoveHelper helper, MoveContext context)
    {
        _context = context;
        _helper = helper;
    }
    
    public void Execute()
    {
        _helper.Move(_context);
    }
}