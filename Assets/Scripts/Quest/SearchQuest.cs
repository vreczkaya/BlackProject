using UnityEngine;

public class SearchQuest : MonoBehaviour, IQuestTemplate
{
    protected bool isFound = false;

    private float angle = 1;
    protected string playersTag = "Player";

    public bool IsQuestStarted { get; set; }

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        isFound = other.tag == playersTag;
        gameObject.SetActive(!isFound);
    }

    public bool IsQuestCompleted(Quest quest)
    {
        return isFound && IsQuestStarted;
    }
}
