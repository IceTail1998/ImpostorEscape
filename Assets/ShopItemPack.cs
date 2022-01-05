using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPack : MonoBehaviour
{
    Animator anim;
    ShopItemButton[] listButton;
    [SerializeField]
    Sprite Sprite_ButtonNormal;
    [SerializeField]
    Sprite Sprite_ButtonSelected;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        listButton = GetComponentsInChildren<ShopItemButton>();
    }
    public void PlayAnim(string animName)
    {
        anim?.Play(animName);
    }
    public void UpdateData(int page)
    {
        int startP = page * 6;
        if (ShopSkinPanel.Instance.currentType == 1)
        {
            SkinData[] list = CharacterAndAccessoryManage.Instance.GetListSkinData();
            for (int i = 0; i < 6; i++)
            {
                int c = i + startP;
                if (c < list.Length)
                {
                    listButton[i].UpdateData(list[c], this);
                    listButton[i].UpdateSprite(Sprite_ButtonNormal);
                    listButton[i].gameObject.SetActive(true);
                }
                else
                {
                    listButton[i].gameObject.SetActive(false);
                }
            }
        }
        else if (ShopSkinPanel.Instance.currentType == 2)
        {
            SkinData[] list = CharacterAndAccessoryManage.Instance.GetListPetData();
            for (int i = 0; i < 6; i++)
            {
                int c = i + startP;
                if (c < list.Length)
                {
                    listButton[i].UpdateData(list[c], this);
                    listButton[i].UpdateSprite(Sprite_ButtonNormal);
                    listButton[i].gameObject.SetActive(true);
                }
                else
                {
                    listButton[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public void SelectButton(int id)
    {
        for (int i = 0; i < listButton.Length; i++)
        {
            if (listButton[i].id == id)
            {
                listButton[i].UpdateSprite(Sprite_ButtonSelected);
            }
            else
            {
                listButton[i].UpdateSprite(Sprite_ButtonNormal);
            }
        }
    }
}
