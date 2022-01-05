using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManage.Instance.FinishLevel();
            Player.Instance.WinAction();
        }
    }
}