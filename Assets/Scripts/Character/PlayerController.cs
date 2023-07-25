using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;
    private KeyboardInput keyboardInput;

    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();
        keyboardInput = GetComponent<KeyboardInput>();
    }

    private void Update()
    {
        keyboardInput.UpdateInput();
        characterMovement.UpdateMovement();
        characterAnimation.UpdateAnimation();
    }
}
