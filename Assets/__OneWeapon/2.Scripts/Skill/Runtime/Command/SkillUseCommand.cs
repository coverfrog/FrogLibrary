public class SkillUseCommand : ICommand
{
    private readonly SkillController _ctrl;
    private readonly SkillContext _context;
    
    public SkillUseCommand(SkillController ctrl, SkillContext context)
    {
        _context = context;
        _ctrl = ctrl;
    }
    
    public void Execute()
    {
        _ctrl.ChangeState(_context);
    }
}