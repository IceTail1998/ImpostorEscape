using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalLevelMap : MonoBehaviour
{
    //[SerializeField]
    //GameObject PiecesDad;
    //[SerializeField]
    //Animator ObjectAnim;
    //Item[] listItem;
    //[SerializeField]
    //GameObject[] listSlot;
    //[SerializeField]
    //bool bIsTutorial = false;
    //[SerializeField]
    //bool bIsSpecial = false;
    //[SerializeField]
    //int objectId = 1;
    //[SerializeField]
    //float camSize = 7f;

    //public float CamSize { get => camSize; set => camSize = value; }

    //public void OnCheckWin()
    //{
    //    for (int i = 0; i < listItem.Length; i++)
    //    {
    //        if (!listItem[i].IsPlaced)
    //        {
    //            return;
    //        }
    //    }
    //    OnFinishLevel();
    //}

    //public void OnFinishLevel()
    //{
    //    if (bIsSpecial)
    //    {
    //        ObjectManage.Instance.OpenObject(objectId);
    //    }
    //    GameController.Instance.EndGame();
    //    StartCoroutine(EndIE());
    //}
    //private IEnumerator EndIE()
    //{
    //    yield return new WaitForSeconds(.4f);
    //    if (ObjectAnim != null)
    //    {
    //        ObjectAnim.Play("in");
    //    }
    //    if (bIsSpecial)
    //    {
    //        yield return new WaitForSeconds(.3f);
    //    }
    //    GameController.Instance.WinLevel();
    //}
    //public void OnStartLevel()
    //{
    //    PiecesDad.SetActive(true);
    //    if (ObjectAnim == null)
    //    {
    //        ObjectAnim = GetComponentInChildren<Animator>();
    //    }
    //    listItem = PiecesDad.GetComponentsInChildren<Item>();
    //    CameraZoom.ResetZoomHandle();
    //    CameraZoom.ZoomSize_Handle(CamSize);
    //    GameController.Instance.StartGame();
    //    if (bIsTutorial)
    //    {
    //        StartCoroutine(StartHintIE());
    //    }
    //}
    //private IEnumerator StartHintIE()
    //{
    //    yield return new WaitForSeconds(.2f);
    //    OnHintShow();
    //}
    //public void Setup()
    //{
    //    CameraZoom.ResetZoomHandle();
    //    CameraZoom.ZoomSize_Handle(CamSize);
    //    if (ObjectAnim == null)
    //    {
    //        ObjectAnim = GetComponentInChildren<Animator>();
    //    }
    //    listItem = PiecesDad.GetComponentsInChildren<Item>();
    //    ObjectAnim.gameObject.SetActive(true);
    //    PiecesDad.SetActive(false);
    //}
    //public void OnHintShow()
    //{
    //    Debug.Log("LevelMap: Hint show");
    //    int wrongIndex = -1;
    //    Item itemWrong = null;
    //    for (int i = 0; i < listSlot.Length; i++)
    //    {
    //        ISlot slotZ = listSlot[i].GetComponent<ISlot>();
    //        if (slotZ.IsPlaced() && !slotZ.IsCorrect())
    //        {
    //            if (wrongIndex == -1)
    //            {
    //                wrongIndex = i;
    //                itemWrong = slotZ.GetItemHold();
    //            }
    //            else
    //            {
    //                Item itemW2 = slotZ.GetItemHold();
    //                ISlot slotZZ;
    //                for (int j = 0; j < listSlot.Length; j++)
    //                {
    //                    slotZZ = listSlot[j].GetComponent<ISlot>();
    //                    if (slotZZ.IsPlaced() && slotZZ.GetItemHold().GetHashCode() == itemW2.GetHashCode())
    //                    {
    //                        slotZZ.UnPlaceItem();
    //                    }
    //                }
    //            }
    //            if (slotZ.IsKey())
    //            {
    //                slotZ.UnPlaceItem();
    //            }
    //            else
    //            {
    //                ISlot slotZZ; 
    //                for (int j = 0; j < listSlot.Length; j++)
    //                {
    //                    slotZZ = listSlot[j].GetComponent<ISlot>();
    //                    if (slotZZ.IsPlaced() && slotZZ.GetItemHold().GetHashCode() == itemWrong.GetHashCode())
    //                    {
    //                        slotZZ.UnPlaceItem();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    if (wrongIndex != -1)
    //    {
    //        for (int i = 0; i < listSlot.Length; i++)
    //        {
    //            ISlot slotZ = listSlot[i].GetComponent<ISlot>();
    //            if (!slotZ.IsPlaced() && slotZ.IsKey() && slotZ.GetID() == itemWrong.ID)
    //            {
    //                itemWrong.OnHintShow();
    //                slotZ.DoCheck(itemWrong);
    //                HintUIManage.Instance.ShowHint(itemWrong, slotZ);
    //                break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < listSlot.Length; i++)
    //        {
    //            ISlot slotZ = listSlot[i].GetComponent<ISlot>();
    //            if (!slotZ.IsPlaced() && slotZ.IsKey())
    //            {
    //                for (int j = 0; j < listItem.Length; j++)
    //                {
    //                    if (!listItem[j].IsPlaced && listItem[j].ID == slotZ.GetID())
    //                    {
    //                        listItem[j].OnHintShow();
    //                        slotZ.DoCheck(listItem[j]);
    //                        HintUIManage.Instance.ShowHint(listItem[j], slotZ);
    //                        break;
    //                    }
    //                }
    //                break;
    //            }
    //        }
    //    }
    //    Debug.Log("LevelMap: Hint show done");

    //}

    //public void TurnOffLevel()
    //{
    //    PiecesDad.SetActive(false);
    //    ObjectAnim.gameObject.SetActive(false);
    //}

    //public void TurnOnLevel()
    //{
    //    PiecesDad.SetActive(true);
    //    ObjectAnim.gameObject.SetActive(true);
    //}
}
