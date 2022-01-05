using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}