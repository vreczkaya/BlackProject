using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus characterStatus;

    [SerializeField]
    private Transform targetLook;

    private Animator animator;
    private WeaponController characterInventory;

    private Transform[] hands = new Transform[2];

    private float[] weights = new float[2];

    private Transform shoulder;
    private Transform aimPivot;

    private string[] handsNames = new string[]
    {
        "Left Hand",
        "Right Hand"
    };

    private Quaternion leftHandRotation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterInventory = GetComponent<WeaponController>();
        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        weights[0] = 1;

        CreateHandsControllers();
    }

    private void CreateHandsControllers()
    {
        aimPivot = new GameObject().transform;
        aimPivot.name = "Aim Pivot";
        aimPivot.transform.parent = transform;

        for (int i = 0; i < hands.Length; i++)
        {
            hands[i] = new GameObject().transform;
            hands[i].name = handsNames[i];
            hands[i].transform.parent = aimPivot;
        }
    }

    private void SetRightHandTransform()
    {
        if (characterStatus.HasWeapon && characterStatus.IsAiming)
        {
            hands[1].localPosition = characterInventory.GetWeaponProperty().rightHandPos;
            Quaternion rotationRight = Quaternion.Euler(characterInventory.GetWeaponProperty().rightHandRot.x,
                characterInventory.GetWeaponProperty().rightHandRot.y,
                characterInventory.GetWeaponProperty().rightHandRot.z);
            hands[1].localRotation = rotationRight;
        }
    }

    private void Update()
    {
        SetRightHandTransform();
        UpdateHandsPosition();
    }

    private void UpdateHandsPosition()
    {
        if (characterStatus.IsAiming)
        {
            SetLeftHandPosition(characterInventory.GetActiveWeapon().leftHandAimTarget.rotation,
                characterInventory.GetActiveWeapon().leftHandAimTarget.position);
        }
        else if (characterStatus.HasWeapon)
        {
            SetLeftHandPosition(characterInventory.GetActiveWeapon().leftHandTarget.rotation,
               characterInventory.GetActiveWeapon().leftHandTarget.position, -1);
        }
        weights[1] = Mathf.Clamp01(weights[1]);
    }

    private void SetLeftHandPosition(Quaternion rotation, Vector3 position, int multiplier = 1)
    {
        leftHandRotation = rotation;
        hands[0].position = position;

        weights[1] += multiplier * Time.deltaTime * 60;
    }

    private void OnAnimatorIK()
    {
        aimPivot.position = shoulder.position;

        if (characterStatus.HasWeapon)
        {
            if (characterStatus.IsAiming)
            {
                aimPivot.LookAt(targetLook);

                animator.SetLookAtWeight(1f, 0f, 1f);
                animator.SetLookAtPosition(targetLook.position);
            }
            else
            {
                animator.SetLookAtWeight(.3f, 0f, .3f);
                animator.SetLookAtPosition(targetLook.position);
            }

            SetIK();
        }
       
    }

    private void SetIK()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            animator.SetIKPositionWeight((AvatarIKGoal)(i + 2), weights[i]);
            animator.SetIKRotationWeight((AvatarIKGoal)(i + 2), weights[i]);
            animator.SetIKPosition((AvatarIKGoal)(i + 2), hands[i].position);
        }
        
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand, hands[1].rotation);
    }
}
