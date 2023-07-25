using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent OnDoorEntered;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnDoorEntered?.Invoke();
        }
    }
}
