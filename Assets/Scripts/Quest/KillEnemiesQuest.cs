using UnityEngine;

public class KillEnemiesQuest : MonoBehaviour, IKillEnemiesQuest
{
    private int killedEnemiesAmount = 0;

    [SerializeField]
    private EnemyStat[] enemyStats;

    [SerializeField, Range(0, 10)]
    private int requiredAmount;

    public bool IsQuestStarted { get; set; }

    private void Start()
    {
        foreach (var stat in enemyStats)
        {
            stat.OnDie.AddListener(AddPeopleAmount);
        }
    }

    public void AddPeopleAmount()
    {
        killedEnemiesAmount++;
    }

    public bool IsQuestCompleted(Quest quest)
    {
        return killedEnemiesAmount >= requiredAmount && IsQuestStarted;
    }

    private void OnDestroy()
    {
        foreach (var stat in enemyStats)
        {
            stat.OnDie.RemoveListener(AddPeopleAmount);
        }
    }

    public int GetKilledAmount()
    {
        return killedEnemiesAmount;
    }
}
