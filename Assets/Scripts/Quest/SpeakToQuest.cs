using System.Collections;
using UnityEngine;

public class SpeakToQuest : GoToQuest
{
    [SerializeField]
    private CharacterToSpeak character;

    private TextHandler textHandler = new TextHandler();

    public bool isTalked = false;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == playersTag && IsQuestStarted && TextHandler.NPCTextQueue.Count != 0)
        {
            character.Talk(TextHandler.NPCTextQueue.Dequeue());
            StartCoroutine(WaitUntilTheEnd());
        }
    }

    private IEnumerator WaitUntilTheEnd()
    {
        yield return new WaitForSeconds(2.8f);
        isTalked = true;
        isCompleted = isTalked;
    }
}
