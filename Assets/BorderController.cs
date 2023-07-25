using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField]
    private SpeakToQuest quest;

    private void Update()
    {
        gameObject.SetActive(!quest.isTalked);
    }
}
