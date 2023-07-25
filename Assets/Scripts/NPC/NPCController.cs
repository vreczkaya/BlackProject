using UnityEngine;

public class NPCController : CharacterToSpeak
{
    private Animator animator;

    [SerializeField]
    private DialogSystem dialogSystem;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Talk(string[] texts)
    {
        dialogSystem.gameObject.SetActive(true);
        dialogSystem.StartTalking(texts);
    }

    public void Update()
    {
        animator.SetBool("isTalking", dialogSystem.IsTalking);
    }
}
