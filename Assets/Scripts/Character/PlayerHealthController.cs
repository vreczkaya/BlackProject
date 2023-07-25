using UnityEngine;

public class PlayerHealthController : Health
{
    [SerializeField]
    private CharacterStatus characterStatus;

    protected override void Die()
    {
        characterStatus.IsDead = true;
    }
}
