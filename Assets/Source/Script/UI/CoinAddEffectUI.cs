using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAddEffectUI : MonoBehaviour
{
    public static CoinAddEffectUI Instance;
    [SerializeField]
    GameObject coinImage;
    List<GameObject> listImageCoin = new List<GameObject>();
    private bool TouchDes;
    [SerializeField]
    float distanceSquareSpawn = .7f;
    [SerializeField]
    int numberOfCoinImage = 20;
    float minX, maxX, minY, maxY;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }
    private void Init()
    {
        while (listImageCoin.Count < numberOfCoinImage)
        {
            GameObject newIM = Instantiate(coinImage, transform);
            listImageCoin.Add(newIM);
        }
    }
    public void ShowEffect(int count, Vector3 start, Vector3 end)
    {
        transform.position = start;
        count = numberOfCoinImage;
        minX = start.x - distanceSquareSpawn;
        maxX = start.x + distanceSquareSpawn;
        minY = start.y - distanceSquareSpawn;
        maxY = start.y + distanceSquareSpawn; ;
        Vector3 tmpScale = transform.localScale;
        transform.localScale = tmpScale * 0.5f;
        for (int i = 0; i < count; i++)
        {
            Vector3 tmp;
            tmp.x = Random.Range(minX, maxX);
            tmp.y = Random.Range(minY, maxY);
            tmp.z = start.z;
            if (i < listImageCoin.Count)
            {
                listImageCoin[i].transform.position = tmp;
                listImageCoin[i].gameObject.SetActive(true);
            }
            else
            {
                GameObject a = Instantiate(coinImage, transform);
                listImageCoin.Add(a);
                a.transform.position = tmp;
                a.SetActive(true);
                //SoundManage.Instance.Play_CoinGain();
            }
            StartCoroutine(ShowAndScale(tmpScale, end));

        }
        //StartCoroutine(ShowIE(count, start, end));
    }
    public void ShowEffect(Vector3 start, Vector3 end)
    {
        bool ok = false;
        for (int i = 0; i < listImageCoin.Count; i++)
        {
            if (!listImageCoin[i].gameObject.activeSelf)
            {
                listImageCoin[i].transform.position = start;
                listImageCoin[i].SetActive(true);
                StartCoroutine(MoveOverSecond(timeCoinFly, listImageCoin[i].transform, end));
                ok = true;
                break;
            }
        }
        if (!ok)
        {
            GameObject a = Instantiate(coinImage, transform);
            a.transform.position = start;
            a.SetActive(true);
            listImageCoin.Add(a);
            StartCoroutine(MoveOverSecond(timeCoinFly, a.transform, end));
            //SoundManage.Instance.Play_CoinPickUp();
        }
    }
    public void ShowEffectNoAppear(int count, Vector3 start, Vector3 end)
    {
        StartCoroutine(ShowIE(count, start, end));

    }
    private IEnumerator ShowAndScale(Vector3 originalScale, Vector3 des)
    {
        Vector3 tmp = transform.localScale, desScale = originalScale * 1.1f;

        float time = 0;
        while (tmp.x < desScale.x)
        {
            time += Time.deltaTime;
            tmp = Vector3.Lerp(tmp, desScale, time / 0.1f);
            transform.localScale = tmp;
            yield return null;
        }
        time = 0;
        while (tmp.x > originalScale.x)
        {
            time += Time.deltaTime;
            tmp = Vector3.Lerp(tmp, originalScale, time / 0.1f);
            transform.localScale = tmp;
            yield return null;
        }
        //SoundManager.instance.BonusUpSound();
        yield return new WaitForSeconds(.3f);
        //TouchDes = false;
        for (int i = 0; i < listImageCoin.Count; i++)
        {
            Vector3 desz = des - listImageCoin[i].transform.position;
            desz = listImageCoin[i].transform.position - 5 * desz.normalized;
            Debug.DrawLine(listImageCoin[i].transform.position, desz, Color.white, 5f);
            StartCoroutine(MoveOverSecond2Point(.4f, timeCoinFly, listImageCoin[i].transform, desz, des));
            yield return new WaitForSeconds(0.05f);
        }
        //yield return new WaitForSeconds(.6f);
        //for (int i = 0; i < listImageCoin.Count; i++)
        //{
        //    StartCoroutine(MoveOverSecond(timeCoinFly, listImageCoin[i].transform, des));
        //    yield return null;
        //}
        //yield return new WaitForSeconds(timeCoinFly);
    }
    [SerializeField]
    float timeCoinFly = 1f;
    private IEnumerator ShowIE(int count, Vector3 start, Vector3 endP)
    {
        bool ok;

        while (count > 0)
        {
            count--;
            ok = false;

            for (int i = 0; i < listImageCoin.Count; i++)
            {
                if (!listImageCoin[i].activeSelf)
                {
                    ok = true;
                    listImageCoin[i].transform.position = start;
                    listImageCoin[i].SetActive(true);
                    StartCoroutine(MoveOverSecond(timeCoinFly, listImageCoin[i].transform, endP));
                    //SoundManage.Instance.Play_CoinPickUp();
                    yield return new WaitForSeconds(0.05f);
                    break;
                }
            }
            if (!ok)
            {
                GameObject a = Instantiate(coinImage, transform);
                listImageCoin.Add(a);
                a.transform.position = start;
                a.SetActive(true);
                //SoundManage.Instance.Play_CoinPickUp();
                StartCoroutine(MoveOverSecond(timeCoinFly, a.transform, endP));
                yield return new WaitForSeconds(0.05f);
            }

        }
    }
    private IEnumerator MoveOverSecond(float sec, Transform moveP, Vector3 des)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.deltaTime;
            moveP.position = Vector3.Lerp(moveP.position, des, t / sec);
            if (Vector3.SqrMagnitude(des - moveP.position) < 0.1f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        moveP.position = des;
        moveP.gameObject.SetActive(false);
    }
    private IEnumerator MoveOverSecond2Point(float sec1, float sec2, Transform moveP, Vector3 des1, Vector3 des2)
    {
        float t = 0;
        Vector3 vel = Vector3.zero;
        while (t < sec1)
        {
            t += Time.deltaTime;
            moveP.position = Vector3.SmoothDamp(moveP.position, des1, ref vel, t / sec1);
            if (moveP.position == des1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        moveP.position = des1;
        t = 0;
        while (t < sec2)
        {
            t += Time.deltaTime;
            moveP.position = Vector3.LerpUnclamped(moveP.position, des2, t / sec2);
            yield return new WaitForEndOfFrame();
        }
        SoundManage.Instance.Play_CoinPickUp();
        moveP.position = des2;
        moveP.gameObject.SetActive(false);
    }

    public void HideAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < listImageCoin.Count; i++)
        {
            listImageCoin[i].SetActive(false);
        }
    }
    public void Show(Vector3 start, Vector3 des)
    {
        transform.position = start;
        for (int i = 0; i < numberOfCoinImage; i++)
        {
            Sequence newSec = DOTween.Sequence();
            Vector3 randPoint = Random.insideUnitCircle * distanceSquareSpawn;
            randPoint.z = 0;
            randPoint += transform.position;
            if (i < listImageCoin.Count)
            {
                listImageCoin[i].transform.localPosition = Vector3.zero;

                newSec.Append(listImageCoin[i].transform.DOMove(randPoint, Random.Range(.15f, .3f)));
                listImageCoin[i].gameObject.SetActive(true);
                newSec.AppendInterval(Random.Range(.3f, .5f));
                newSec.Append(listImageCoin[i].transform.DOMove(des, .5f));
            }
            else
            {
                GameObject a = Instantiate(coinImage, transform);
                listImageCoin.Add(a);
                a.transform.localPosition = Vector3.zero;
                newSec.Append(a.transform.DOMove(randPoint, Random.Range(.15f, .3f)));
                newSec.AppendInterval(Random.Range(.3f, .5f));
                newSec.Append(a.transform.DOMove(des, .5f));
                a.SetActive(true);
                //SoundManage.Instance.Play_CoinGain();
            }
        }
        Invoke("HideAll", 1.4f);
    }
}
