using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeShopComponent : MonoBehaviour
{
    [SerializeField]
    float minDeltaX = 50f;
    bool bIsTouching = false;
    Vector2 startPos;
    [SerializeField]
    ShopItemPack[] listComponentAnimator; // use 2 only
    int currentIndex;
    int currentPage = 0;
    int maxPage = 0;
    [SerializeField]
    List<Image> listPageIcon;
    [SerializeField]
    Sprite Sprite_CurrentPage;
    [SerializeField]
    Sprite Sprite_NormalPage;
    [SerializeField]
    GameObject ComingSoonPart;
    [SerializeField]
    Transform topBorder;
    [SerializeField]
    Transform botBorder;
    [SerializeField]
    RectTransform botBorderRect;
    [SerializeField]
    RectTransform topBorderRect;
    private void Start()
    {
        currentIndex = 0;
        currentPage = 0;
    }
    public void OnPointerDownAction()
    {
        if (Input.touchCount == 0) return;
        if (ComingSoonPart.activeInHierarchy) return;
        bIsTouching = true;
        startPos = Input.GetTouch(0).position;
    }
    public void Setup()
    {
        if (ShopSkinPanel.Instance.currentType == 1)
        {
            maxPage = CharacterAndAccessoryManage.Instance.GetListSkinCount();
            if (maxPage % 6 == 0)
            {
                maxPage = maxPage / 6;
            }
            else
            {
                maxPage = maxPage / 6 + 1;
            }
        }
        else if (ShopSkinPanel.Instance.currentType == 2)
        {
            maxPage = 1;
            //maxPage =maxPage = 1; CharacterAndAccessoryManage.Instance.GetListPetCount();
            //if (maxPage % 6 == 0)
            //{
            //    maxPage = maxPage / 6;
            //}
            //else
            //{
            //    maxPage = maxPage / 6 + 1;
            //}
        }
        else
        {
            maxPage = 1;
        }
        listComponentAnimator[currentIndex].UpdateData(currentPage);
        UpdateListPage();
    }
    public void OnPointerUpAction()
    {
        if (bIsTouching)
        {
            bIsTouching = false;
            Vector2 deltaPos = Input.GetTouch(0).position - startPos;
            if (Mathf.Abs(deltaPos.x) > minDeltaX && maxPage > 1)
            {
                if (deltaPos.x > 0)
                {
                    currentPage--;
                    if (currentPage < 0)
                    {
                        currentPage = maxPage - 1;
                    }
                    listComponentAnimator[currentIndex].PlayAnim("outRight");
                    listPageIcon[currentIndex].sprite = null;
                    listPageIcon[currentIndex].sprite = Sprite_NormalPage;
                    currentIndex = (currentIndex + 1) % 2;
                    listPageIcon[currentIndex].sprite = null;
                    listPageIcon[currentIndex].sprite = Sprite_CurrentPage;
                    listComponentAnimator[currentIndex].UpdateData(currentPage);
                    listComponentAnimator[currentIndex].gameObject.SetActive(true);
                    listComponentAnimator[currentIndex].PlayAnim("inLeft");
                }
                else
                {

                    currentPage++;
                    currentPage = currentPage % maxPage;
                    listComponentAnimator[currentIndex].PlayAnim("outLeft");
                    listPageIcon[currentIndex].sprite = null;
                    listPageIcon[currentIndex].sprite = Sprite_NormalPage;
                    currentIndex = (currentIndex + 1) % 2;
                    listPageIcon[currentIndex].sprite = null;
                    listPageIcon[currentIndex].sprite = Sprite_CurrentPage;
                    listComponentAnimator[currentIndex].UpdateData(currentPage);
                    listComponentAnimator[currentIndex].gameObject.SetActive(true);
                    listComponentAnimator[currentIndex].PlayAnim("inRight");
                }
            }
        }
    }
    private void UpdateListPage()
    {
        for (int i = 0; i < maxPage; i++)
        {
            listPageIcon[i].gameObject.SetActive(true);
        }
        for (int i = maxPage; i < listPageIcon.Count; i++)
        {
            listPageIcon[i].gameObject.SetActive(false);
        }
        listPageIcon[currentPage].sprite = null;
        listPageIcon[currentPage].sprite = Sprite_CurrentPage;
    }
    public void ShowComingSoon()
    {
        maxPage = 1;
        for (int i = 0; i < listComponentAnimator.Length; i++)
        {
            listComponentAnimator[i].gameObject.SetActive(false);
        }
        UpdateListPage();
        ComingSoonPart.SetActive(true);
    }
    public void HideComingSoon()
    {
        for (int i = 0; i < listComponentAnimator.Length; i++)
        {
            listComponentAnimator[i].gameObject.SetActive(true);
        }
        ComingSoonPart.SetActive(false);
    }
    Vector2 touchPos;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            touchPos = CameraController.instance.uiCam.ScreenToWorldPoint(touchPos);
            //if (Input.GetTouch(0).position.y > botBorder.position.y && Input.GetTouch(0).position.y < topBorder.position.y)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (touchPos.y > botBorderRect.anchoredPosition.y && touchPos.y < topBorderRect.anchoredPosition.y)
                {
                    OnPointerDownAction();
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                OnPointerUpAction();
            }
        }

    }
}
