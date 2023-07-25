using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private QuestSystem questSystem;

    private int currentSceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            currentSceneIndex = 0;
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            currentSceneIndex = 1;
            questSystem.OnLevelCompleted.AddListener(LoadNextLevel);
        }
        else
        {
            currentSceneIndex = 2;
            questSystem.OnLevelCompleted.AddListener(LoadNextLevel);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        if (currentSceneIndex != 2)
        {
            currentSceneIndex++;
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            currentSceneIndex = 0;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}