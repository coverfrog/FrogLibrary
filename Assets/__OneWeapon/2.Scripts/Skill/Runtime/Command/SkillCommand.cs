public class SkillCommand : ICommand
{
    private SkillController _controller;
    
    public SkillCommand(SkillController controller, SkillContext context)
    {
        _controller = controller;
    }
    
    public void Execute()
    {
        _controller.Transition(SkillStateType.Progress);
    }
}