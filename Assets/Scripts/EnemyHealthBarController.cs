using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField]
    private Slider healthController;

    [SerializeField]
    private EnemyStat enemyStat;

    private void Start()
    {
        healthController.maxValue = enemyStat.MaxHealth;
    }

    private void Update()
    {
        healthController.value = enemyStat.CurrentHealth;
    }
}
