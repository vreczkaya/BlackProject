using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus characterStatus;

    private Animator animator;
    private CharacterMovement characterMovement;
    private KeyboardInput keyboardInput;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        keyboardInput = GetComponent<KeyboardInput>();
        keyboardInput.OnSpaceButtonPressed.AddListener(Jump);

        characterStatus.IsDead = false;
        characterStatus.IsAiming = false;
        characterStatus.HasWeapon = false;
        characterStatus.WeaponType = WeaponType.None;
    }

    public void UpdateAnimation()
    {
        animator.SetBool("isSprinting", characterStatus.IsSprinting);
        animator.SetBool("isAiming", characterStatus.IsAiming);
        animator.SetBool("hasWeapon", characterStatus.HasWeapon);

        if (!characterStatus.IsAiming || !characterStatus.HasWeapon)
        {
            AnimateSimpleMovement();
        }
        else 
        {
            AnimateAiming();
        }
        if (characterStatus.IsDead)
        {
            Die();
        }
    }

    private void Jump()
    {
        animator.SetTrigger("jump");
    }

    private void AnimateSimpleMovement()
    {
        animator.SetFloat("vertical", characterMovement.MoveAmount, 0.1f, Time.deltaTime);
    }

    private void AnimateAiming()
    {
        float vertical = characterMovement.Vertical;

        animator.SetFloat("vertical", vertical, 0.15f, Time.deltaTime);
    }

    public void Die()
    {
        animator.SetTrigger("death");
    }

    private void OnDestroy()
    {
        keyboardInput.OnSpaceButtonPressed.RemoveListener(Jump);
    }
}
