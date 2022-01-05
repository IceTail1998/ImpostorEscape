using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAndAccessoryManage : MonoBehaviour
{
    public static CharacterAndAccessoryManage Instance;
    [SerializeField]
    SkinData[] listSkinData;
    [SerializeField]
    SkinData[] listPetData;
    [SerializeField]
    MeshRenderer characterMesh1;
    [SerializeField]
    SkinnedMeshRenderer characterMesh;
    [SerializeField]
    MeshRenderer Shop_characterMesh1;
    [SerializeField]
    SkinnedMeshRenderer Shop_characterMesh;
    [SerializeField]
    Animator SkinPreviewAnimator;
    [SerializeField]
    Vector3 RotateDefaultPreview = new Vector3(5, 190, 0);
    [SerializeField]
    Transform rotateObjectPreview;
    const string PREFIX_SKIN = "SKIN_";
    const string PREFIX_PET = "PET_";
    public int currentSkinId { get; private set; }
    public int currentPetId { get; private set; }
    Shader normalShader;
    Shader transparentShader;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (FirstOpenController.instance.IsOpenFirst)
        {
            FirstInit();
        }
        CheckSkinAndPet();
        normalShader = Shader.Find("Legacy Shaders/Bumped Specular");
        transparentShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
    }
    public void FirstInit()
    {
        for (int i = 0; i < listSkinData.Length; i++)
        {
            PlayerPrefs.SetInt(PREFIX_SKIN + listSkinData[i].id, 0);
        }
        PlayerPrefs.SetInt(PREFIX_SKIN + listSkinData[0].id, 1);
        PlayerPrefs.SetInt(PREFIX_SKIN + "SELECTED", listSkinData[0].id);
        for (int i = 0; i < listPetData.Length; i++)
        {
            PlayerPrefs.SetInt(PREFIX_PET + listPetData[i].id, 0);
        }
    }
    public List<int> GetListIdSkinLock()
    {
        List<int> lisRet = new List<int>();
        for (int i = 0; i < listSkinData.Length; i++)
        {
            int c = i;
            if (!IsSkinOpenned(c))
            {
                lisRet.Add(c);
            }
        }
        return lisRet;
    }
    public SkinData GetRandomSkinData()
    {
        int rand = Random.Range(0, listSkinData.Length);
        if (rand >= 0 && rand < listSkinData.Length)
            return listSkinData[rand];
        return null;
    }
    public SkinData[] GetListSkinData()
    {
        return listSkinData;
    }
    public SkinData[] GetListPetData()
    {
        return listPetData;
    }
    public void PreviewSkin(int skinId)
    {
        if (skinId < 0 || skinId >= listSkinData.Length) return;
        if (rotateObjectPreview != null)
        {
            Quaternion qua = rotateObjectPreview.localRotation;
            qua.eulerAngles = RotateDefaultPreview;
            rotateObjectPreview.localRotation = qua;
        }
        Shop_characterMesh.sharedMaterial = listSkinData[skinId].material;
        Shop_characterMesh1.sharedMaterial = listSkinData[skinId].material;
        for (int i = 0; i < listSkinData.Length; i++)
        {
            for (int j = 0; j < listSkinData[i].listObjectPreview.Length; j++)
            {
                if (i == skinId)
                {
                    listSkinData[i].listObjectPreview[j].SetActive(true);
                }
                else
                {
                    listSkinData[i].listObjectPreview[j].SetActive(false);
                }
            }
        }
        SkinPreviewAnimator.Play(listSkinData[skinId].animationName);
    }

    internal int GetLowestPrice()
    {
        int lowestPrice = 999999;
        for (int i = 0; i < listSkinData.Length; i++)
        {
            if (!IsSkinOpenned(listSkinData[i].id))
            {
                if (lowestPrice > listSkinData[i].price)
                {
                    lowestPrice = listSkinData[i].price;
                }
            }
        }
        return lowestPrice;
    }

    public void OnPurchaseSkin(int id)
    {
        OpenSkin(id);
        SelectSkin(id);
        CheckSkinAndPet();
    }
    public void OnPurchasePet(int id)
    {
        OpenPet(id);
        SelectPet(id);
    }
    public void PreviewPet(int petId)
    {

    }
    [SerializeField]
    int IAPSkinID = 5;
    public void IAPReward()
    {
        OnPurchaseSkin(IAPSkinID);
    }
    public void SelectSkin(int skinId)
    {
        currentSkinId = skinId;
        PreviewSkin(skinId);
        PlayerPrefs.SetInt(PREFIX_SKIN + "SELECTED", skinId);
    }
    public void SelectPet(int petId)
    {
        currentPetId = petId;
        PlayerPrefs.SetInt(PREFIX_PET + "SELECTED", petId);
    }
    public void CheckSkinAndPet()
    {
        int id = PlayerPrefs.GetInt(PREFIX_SKIN + "SELECTED", 0);
        currentSkinId = id;
        if (id < 0 || id > listSkinData.Length) return;
        characterMesh.sharedMaterial = listSkinData[id].material;
        characterMesh1.sharedMaterial = listSkinData[id].material;
        Shop_characterMesh.sharedMaterial = listSkinData[id].material;
        Shop_characterMesh1.sharedMaterial = listSkinData[id].material;
        for (int i = 0; i < listSkinData.Length; i++)
        {
            for (int j = 0; j < listSkinData[i].listObject.Length; j++)
            {
                if (i == id)
                {
                    listSkinData[i].listObject[j].SetActive(true);
                }
                else
                {
                    listSkinData[i].listObject[j].SetActive(false);
                }
            }
            for (int j = 0; j < listSkinData[i].listObjectPreview.Length; j++)
            {
                if (i == id)
                {
                    listSkinData[i].listObjectPreview[j].SetActive(true);
                }
                else
                {
                    listSkinData[i].listObjectPreview[j].SetActive(false);
                }
            }
        }
    }
    public SkinData GetSkinData(int id)
    {
        if (id < 0 || id >= listSkinData.Length)
        {
            return null;
        }
        return listSkinData[id];
    }
    public SkinData GetPetData(int id)
    {
        if (id < 0 || id >= listPetData.Length)
        {
            return null;
        }
        return listPetData[id];
    }
    public int GetListSkinCount()
    {
        return listSkinData.Length;
    }
    public int GetListPetCount()
    {
        return listPetData.Length;
    }
    public bool IsSkinOpenned(int id)
    {
        return PlayerPrefs.GetInt(PREFIX_SKIN + id, 0) == 1;
    }
    public void OpenSkin(int id)
    {
        PlayerPrefs.SetInt(PREFIX_SKIN + id, 1);
    }
    public bool IsPetOpenned(int id)
    {
        return PlayerPrefs.GetInt(PREFIX_PET + id, 0) == 1;
    }
    public void OpenPet(int id)
    {
        PlayerPrefs.SetInt(PREFIX_PET + id, 1);
    }
    public void DoHide()
    {
        characterMesh.sharedMaterial.shader = transparentShader;
        Color color = new Color(1, 1, 1, .4f);
        characterMesh.sharedMaterial.SetColor("_Color", color);
        characterMesh1.sharedMaterial.SetColor("_Color", color);
        characterMesh1.sharedMaterial.shader = transparentShader;
        for (int i = 0; i < listSkinData.Length; i++)
        {
            for (int j = 0; j < listSkinData[i].listObject.Length; j++)
            {
                if (listSkinData[i].id == currentSkinId)
                {
                    listSkinData[i].listObject[j].GetComponent<MeshRenderer>().sharedMaterial.shader = transparentShader;
                    listSkinData[i].listObject[j].GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", color);
                }
                else
                {
                    listSkinData[i].listObject[j].SetActive(false);
                }
            }
        }
    }
    public void EndHide()
    {
        characterMesh.sharedMaterial.shader = normalShader;
        characterMesh1.sharedMaterial.shader = normalShader;
        Color color = new Color(1, 1, 1, 1);
        characterMesh.sharedMaterial.SetColor("_Color", color);
        characterMesh1.sharedMaterial.SetColor("_Color", color);
        for (int i = 0; i < listSkinData.Length; i++)
        {
            for (int j = 0; j < listSkinData[i].listObject.Length; j++)
            {
                if (listSkinData[i].id == currentSkinId)
                {
                    listSkinData[i].listObject[j].GetComponent<MeshRenderer>().sharedMaterial.shader = normalShader;
                    listSkinData[i].listObject[j].GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", color);
                }
                else
                {
                    listSkinData[i].listObject[j].SetActive(false);
                }
            }
        }
    }

}
[System.Serializable]
public class SkinData
{
    public int id;
    public string name;
    public string animationName;
    public Sprite icon;
    public int price;
    public GameObject[] listObject;
    public GameObject[] listObjectPreview;
    public Material material;
}

