using TMPro;
using UnityEngine;

public class resourceTextUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro;

    //used so the pilelevel can be taken from thiss class
    [SerializeField]
    ResourcePileManager resourcePileManager;

    //used to display how much resources are in the pile
    float resourceCount;

    void Update()
    {
        resourceCount = resourcePileManager.pileLevel;
        textMeshPro.text = "Press[R] to collect " + resourceCount  + " resources";
    }
}
