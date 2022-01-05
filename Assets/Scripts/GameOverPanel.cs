using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject replayBtn;
    [SerializeField] private float timeDelay;

    private void Start()
    {
        DucPhung.GameController.instance.ReplayGame += ReplayGame;
    }

    private void OnEnable()
    {
        replayBtn.SetActive(false);
        StartCoroutine(DelayDisplayBtn());
    }

    private void ReplayGame()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DelayDisplayBtn()
    {
        yield return new WaitForSeconds(timeDelay);

        replayBtn.SetActive(true);
    }
}