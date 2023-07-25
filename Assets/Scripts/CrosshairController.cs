using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField]
    private Canvas reticle;
    [SerializeField]
    private CharacterStatus characterStatus;

    private void Start()
    {
        reticle.gameObject.SetActive(false);
    }

    private void Update()
    {
        reticle.gameObject.SetActive(characterStatus.IsAiming);
    }
}
