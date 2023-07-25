using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ViewModel: MonoBehaviour
{
    [SerializeField]
    private QuestSystem questSystem;

    [SerializeField]
    private PlayerHealthController healthController;

    [SerializeField]
    private CharacterStatus characterStatus;

    public string GetQuestDescription => questSystem.GetCurrentQuestDescription();

    public int KilledEnemiesAmount => questSystem.GetAmount();

    public float GetHealth => healthController.CurrentHealth;

    [HideInInspector]
    public UnityEvent OnQuestStarted;
    [HideInInspector]
    public UnityEvent OnQuestFinished;

    public void SetAimingStatus(bool isAiming = false)
    {
        characterStatus.IsAiming = isAiming;
    }

    private void Start()
    {
        questSystem.OnQuestStarted.AddListener(StartQuest);
    }

    public void StartQuest()
    {
        StartCoroutine(WaitAndStartQuest());
    }

    private IEnumerator WaitAndStartQuest()
    {
        yield return new WaitForSeconds(1.5f);
        OnQuestStarted?.Invoke();
    }
}
