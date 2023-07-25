using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public bool IsTalking;

    public void StartTalking(string[] texts)
    {
        if (!IsTalking)
        {
            StartCoroutine(Talk(texts));
        }
    }

    private IEnumerator Talk(string[] texts)
    {
        IsTalking = true;
        foreach (var phrase in texts)
        {
            text.text = string.Empty;
            foreach (var word in phrase)
            {
                text.text += word;
                yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            }
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.SetActive(false);
        IsTalking = false;
    }
}
