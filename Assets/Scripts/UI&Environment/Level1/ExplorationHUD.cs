using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ExplorationHUD : MonoBehaviour
{
    GameObject remnant1, remnant2;

    public static ExplorationHUD expHUD;
    private void Start()
    {
        expHUD = this;//singleton for the scene
        remnant1 = transform.Find("r1").gameObject;
        remnant2 = transform.Find("r2").gameObject;
        remnant1.SetActive(false);
        remnant1.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PlayerCommonStatus.getRemnantDescriptionWithSceneIndex(0);//Scene index alr set on each remnant
        remnant2.SetActive(false);
        remnant2.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PlayerCommonStatus.getRemnantDescriptionWithSceneIndex(1);
    }
    public void showRemnant(int remsSceneIndex)
    {
        if(remsSceneIndex == 0)
        {
            remnant1.SetActive(true);
        }
        else if(remsSceneIndex == 1)
        {
            remnant2.SetActive(true);
        }    
    }
}
