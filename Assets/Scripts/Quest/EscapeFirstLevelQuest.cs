using UnityEngine;

public class EscapeFirstLevelQuest : SearchQuest
{
    [SerializeField]
    private Door door;

    private bool isOpen = false;

    public new bool IsQuestStarted { get; set; }

    private void Start()
    {
        door.OnDoorEntered.AddListener(OpenDoor);
        IsQuestStarted = false;
    }

    private void OpenDoor()
    {
        isOpen = isFound;
    }

    public new bool IsQuestCompleted(Quest quest)
    {
        return isFound && isOpen;
    }
}
