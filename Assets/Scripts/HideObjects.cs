using UnityEngine;

public class HideObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private void Start()
    {
        DucPhung.GameController.instance.WinGame += WinGame;
    }

    private void OnDisable()
    {
        DucPhung.GameController.instance.WinGame -= WinGame;
    }

    private void WinGame()
    {
        foreach (GameObject obj in objects) obj.SetActive(false);
    }
}