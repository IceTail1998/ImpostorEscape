using UnityEngine;
using System.Collections;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private float timeDelay;

    private void Start()
    {
        DucPhung.GameController.instance.NextLevel += NextLevel;
    }

    private void OnEnable()
    {
        nextLevel.SetActive(false);
        StartCoroutine(DelayDisplayBtn());
    }

    private IEnumerator DelayDisplayBtn()
    {
        yield return new WaitForSeconds(timeDelay);
        nextLevel.SetActive(true);
    }

    private void NextLevel()
    {
        this.gameObject.SetActive(false);
    }
}