using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestSystem : MonoBehaviour
{
    private Quest currentQuest;

    [SerializeField]
    private SpeakToQuest firstQuest;
    [SerializeField]
    private SearchQuest secondQuest;
    [SerializeField]
    private SpeakToQuest thirdQuest;
    [SerializeField]
    private KillEnemiesQuest fourthQuest;
    [SerializeField]
    private SearchQuest fifthQuest;
    [SerializeField]
    private GoToQuest sixthQuest;

    private Queue<IQuestTemplate> quests;

    [HideInInspector]
    public UnityEvent OnQuestStarted;
    [HideInInspector]
    public UnityEvent OnLevelCompleted;

    [SerializeField]
    private List<GameObject> questsList;

    [SerializeField]
    private int questIndex = 0;

    private TextHandler textHandler = new TextHandler();

    private string currentQuestDescription;

    private void Awake()
    {
        PrepareQuestQueues();

        questsList[questIndex].SetActive(true);
        currentQuest = new Quest(quests.Dequeue());
        currentQuestDescription = TextHandler.QuestTextQueue.Dequeue()[0];

        OnQuestStarted?.Invoke();
    }

    private void PrepareQuestQueues()
    {
        quests = new Queue<IQuestTemplate>();
       
        quests.Enqueue(firstQuest);
        quests.Enqueue(secondQuest);
        quests.Enqueue(thirdQuest);
        quests.Enqueue(fourthQuest);
        quests.Enqueue(fifthQuest);
        quests.Enqueue(sixthQuest);
    }

    private void Update()
    {
        if (currentQuest.IsQuestComplete())
        {
            SwitchQuests();
            if (quests.Count != 0)
            {
                currentQuest = new Quest(quests.Dequeue());
                OnQuestStarted?.Invoke();
            }
        }
    }

    public Quest GetCurrentQuest() => currentQuest;

    private void SwitchQuests()
    {
        questsList[questIndex].SetActive(false);
        if (questIndex == questsList.Count - 1)
        {
            OnLevelCompleted?.Invoke();
        }
        else
        {
            questIndex++;
            currentQuestDescription = TextHandler.QuestTextQueue.Dequeue()[0];
            questsList[questIndex].SetActive(true);
        }
    }

    public int GetAmount() => currentQuest.GetAmount();

    public string GetCurrentQuestDescription() => currentQuestDescription;
}
