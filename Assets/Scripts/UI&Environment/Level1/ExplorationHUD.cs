using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ExplorationHUD : MonoBehaviour
{
    GameObject remnant1, remnant2;
    int remnantCount = 2;
    public static ExplorationHUD expHUD;
    private void Start()
    {
        expHUD = this;//singleton for the scene
        remnant1 = transform.Find("r1").gameObject;
        remnant2 = transform.Find("r2").gameObject;
        remnant1.SetActive(false);       
        remnant2.SetActive(false);

        Invoke("uiUpdate", 0.1f);
    }

    public void uiUpdate()
    {
        remnant1.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PlayerCommonStatus.getRemnantDescriptionWithSceneIndex(0);
        remnant1.GetComponent<Image>().sprite = PlayerCommonStatus.getRemnantSpriteWithSceneIndex(0);

        remnant2.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PlayerCommonStatus.getRemnantDescriptionWithSceneIndex(1);
        //Scene index alr set on each remnant
        for (int x = 0; x < remnantCount; x++)
        {
            string currentRemnantName = PlayerCommonStatus.getRemnantNameWithSceneIndex(x);
            if (PlayerCommonStatus.checkIfRemnantExist(currentRemnantName))
            {
                if (PlayerCommonStatus.checkRemnantAcquired(currentRemnantName))
                {
                    showRemnant(x);
                }
            }          
        }
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
