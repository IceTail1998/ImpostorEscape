using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimation : MonoBehaviour
{
    int curIndex = 0;
    // Update is called once per frame
    float timer = 5f;
    [SerializeField]
    Animator animator;
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            animator.SetBool("isChange", false);
        }
        else
        {
            timer = Random.Range(6f, 12f);
            curIndex = Random.Range(1, 13);
            curIndex = Mathf.Clamp(curIndex, 1, 12);
            curIndex = Mathf.Clamp(curIndex, 1, 12);
            animator.SetInteger("index", curIndex);
            animator.SetBool("isChange", true);
        }
    }
}
