public class MoveJumpCommand : ICommand
{
    private readonly MoveHelper _helper;
    private readonly MoveContext _context;
    
    public MoveJumpCommand(MoveHelper helper, MoveContext context)
    {
        _context = context;
        _helper = helper;
    }
    
    public void Execute()
    {
        _helper.Jump(_context);
    }
}