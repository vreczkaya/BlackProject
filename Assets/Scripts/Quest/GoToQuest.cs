using UnityEngine;

public class GoToQuest : MonoBehaviour, IQuestTemplate
{
    protected string playersTag = "Player";

    protected bool isCompleted = false;

    public bool IsQuestStarted { get; set; }

    public bool IsQuestCompleted(Quest quest)
    {
        return isCompleted;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        isCompleted = other.tag == playersTag && IsQuestStarted;
    }
}
