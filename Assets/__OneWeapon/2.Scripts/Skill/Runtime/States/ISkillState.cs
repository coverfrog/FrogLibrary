public interface ISkillState : ISimpleState<SkillController>
{
    SkillStateType StateType { get; }
}