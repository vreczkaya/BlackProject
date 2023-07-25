public interface IQuestTemplate
{
    public bool IsQuestStarted { get; set; }
    public abstract bool IsQuestCompleted(Quest quest);
}
