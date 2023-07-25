using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ViewModel viewModel;

    [SerializeField]
    private Text killedEnemiesAmount;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private GameObject questPanel;

    private Text questDescription;

    private string trackingKilledAmount = "Убито врагов: ";

    private void Start()
    {
        questDescription = questPanel.GetComponentInChildren<Text>();
        viewModel.OnQuestStarted.AddListener(ShowQuestDescription);
    }

    private void Update()
    {
        if (viewModel.KilledEnemiesAmount == 0)
        {
            killedEnemiesAmount.gameObject.SetActive(false);
        }
        else
        {
            killedEnemiesAmount.gameObject.SetActive(true);
            killedEnemiesAmount.text = trackingKilledAmount + viewModel.KilledEnemiesAmount.ToString();
        }
        healthBar.value = viewModel.GetHealth;
    }

    public void ShowQuestDescription()
    {
        questDescription.text = viewModel.GetQuestDescription;
        StartCoroutine(StartShowingQuestDescription());
    }

    private IEnumerator StartShowingQuestDescription()
    {
        questPanel.SetActive(true);
        viewModel.SetAimingStatus();
        yield return new WaitForSeconds(2f);
        questPanel.SetActive(false);
    }

    //[SerializeField]
    //private KillEnemiesQuest firstQuest;
    //[SerializeField]
    //private SearchQuest secondQuest;

    //[SerializeField]
    //private Button closeInstructtionsButton;

    //[SerializeField]
    //private GameObject instructionsPanel;
    //[SerializeField]
    //private GameObject theFirstQuestPanel;
    //[SerializeField]
    //private GameObject questFinishedPanel;
    //[SerializeField]
    //private GameObject theSecondQuestPanel;
    //[SerializeField]
    //private GameObject levelPassedPanel;

    //[SerializeField]
    //private Text killCount;
    //[SerializeField]
    //private Text currentLevel;

    

    //[SerializeField]
    //private PlayerHealthController healthController;


    //private void Start()
    //{
    //    firstQuest.OnKilledEnemy.AddListener(ChangeKilledEnemiesText);
    //    firstQuest.OnQuestComplited.AddListener(FinishTheFirstQuest);
    //    firstQuest.OnQuestStated.AddListener(StartFirstQuest);



    //    healthBar.maxValue = healthController.MaxHealth;
    //}

    //private void Update()
    //{
    //    healthBar.value = healthController.CurrentHealth;
    //    currentLevel.text = "level " + Level.currentLevel;
    //}

    //private void TurnOffObject(GameObject panel)
    //{
    //    panel.gameObject.SetActive(false);
    //}

    //private void CloseTheFirstQuest()
    //{
    //    TurnOffObject(theFirstQuestPanel);
    //    instructionsPanel.SetActive(true);
    //    killCount.gameObject.SetActive(true);
    //}

    //private void StartFirstQuest()
    //{
    //    theFirstQuestPanel.SetActive(true);
    //}

    //private void ChangeKilledEnemiesText()
    //{
    //    killCount.text = "Убито врагов: " + firstQuest.KilledEnemiesAmount + " / 3";
    //}

    //private void FinishTheFirstQuest()
    //{
    //    TurnOffObject(killCount.gameObject);
    //    questFinishedPanel.SetActive(true);
    //    StartCoroutine("WaitBeforeTheNextPanel");  
    //}

    //private IEnumerator WaitBeforeTheNextPanel()
    //{
    //    yield return new WaitForSeconds(5f);

    //    questFinishedPanel.SetActive(false);
    //    theSecondQuestPanel.SetActive(true);
    //}

    //private void FinishSecondQuest()
    //{
    //    if (secondQuest.IsQuestComplete(secondQuest))
    //    {
    //        Level.currentLevel++;
    //        levelPassedPanel.SetActive(true);
    //        StartCoroutine(WaitBeforNewLevel());
    //    }
    //}

    //private IEnumerator WaitBeforNewLevel()
    //{
    //    yield return new WaitForSeconds(7f);
    //    SceneManager.LoadScene("PazzleLevel");
    //}

    //private void OnDisable()
    //{
    //    firstQuest.OnKilledEnemy.RemoveListener(ChangeKilledEnemiesText);
    //    firstQuest.OnQuestComplited.AddListener(FinishTheFirstQuest);
    //}
}
