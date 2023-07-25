public class Quest
{
    private IQuestTemplate completionStrategy;

    public Quest(IQuestTemplate completionStrategy)
    {
        this.completionStrategy = completionStrategy;
        this.completionStrategy.IsQuestStarted = true;
    }

    public bool IsQuestComplete() => completionStrategy.IsQuestCompleted(this);

    public bool IsQuestStarted() => completionStrategy.IsQuestStarted;

    public int GetAmount()
    {
        if (completionStrategy is IKillEnemiesQuest)
        {
            IKillEnemiesQuest strategy = (IKillEnemiesQuest)completionStrategy;
            return strategy.GetKilledAmount();
        }
        return 0;
    }

    public IQuestTemplate GetCompletionStrategy() => completionStrategy;
}
