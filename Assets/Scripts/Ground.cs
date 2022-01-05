using UnityEngine;

public class Ground : MonoBehaviour
{
    public static Ground Instance;
    [SerializeField] private Color[] groundColors;

    private MeshRenderer meshRenderer;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(Instance != null)
        {
            Instance = this;
        }
    }

    public void ChangeBG()
    {
        int rd = Random.Range(0, groundColors.Length);
        meshRenderer.sharedMaterial.color = groundColors[rd];
    }

    private void WinGame()
    {
        meshRenderer.enabled = false;
    }

    private void NextLevel()
    {
        meshRenderer.enabled = true;
    }
}