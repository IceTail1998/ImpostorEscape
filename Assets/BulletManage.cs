using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{
    public static BulletManage Instance;
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    List<Bullet> listBullet;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Shoot(Vector3 pos, Vector3 dir)
    {
        for (int i = 0; i < listBullet.Count; i++)
        {
            if (!listBullet[i].gameObject.activeInHierarchy)
            {
                listBullet[i].transform.position = pos;
                listBullet[i].transform.forward = dir;
                listBullet[i].gameObject.SetActive(true);
                return;
            }
        }
        Bullet b = Instantiate(bulletPrefab, pos, Quaternion.identity, transform);
        b.transform.forward = dir;
        b.gameObject.SetActive(true);
        listBullet.Add(b);
    }
    public void TurnOffAllBullet()
    {
        for (int i = 0; i < listBullet.Count; i++)
        {
            if (listBullet[i].gameObject.activeInHierarchy)
            {
                listBullet[i].gameObject.SetActive(false);
            }
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        Shoot(transform.position, transform.forward);
    //    }
    //}
    // Update is called once per frame
}
