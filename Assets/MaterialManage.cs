using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManage : MonoBehaviour
{
    public static MaterialManage Instance;
    [SerializeField]
    Material transperentMaterial;
    [SerializeField]
    Color selectingColor;
    [SerializeField]
    Color normalColor;
    [SerializeField]
    List<PieceMaterial> listPieceMaterial;
    public int currentIndex;
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        currentIndex = 0;
        gameCount = 0;
    }

    public Material GetDefaultMaterial()
    {
        return listPieceMaterial[currentIndex].defaultMaterial;
    }
    public Material GetBackGroundMaterial()
    {
        return listPieceMaterial[currentIndex].backGroundMaterial;
    }
    public Material GetSelectMaterial()
    {
        return listPieceMaterial[currentIndex].selectMaterial;
    }
    //public Material GetPlacedMaterial()
    //{
    //    return listPieceMaterial[currentIndex].placedMaterial;
    //}
    public Material GetTransMaterial()
    {
        return transperentMaterial;
    }
    int gameCount = 0;
    public void ChangePack()
    {
        gameCount++;
        if (gameCount < 4) return;
        gameCount = 0;
        int rand = Random.Range(0, listPieceMaterial.Count);
        if(rand == currentIndex)
        {
            currentIndex++;
            currentIndex = currentIndex % listPieceMaterial.Count;
        }
        else
        {
            currentIndex = Mathf.Clamp(rand, 0, listPieceMaterial.Count);
        }

    }
}
[System.Serializable]
public class PieceMaterial
{
    public Material defaultMaterial;
    public Material selectMaterial;
    public Material backGroundMaterial;
    
}
