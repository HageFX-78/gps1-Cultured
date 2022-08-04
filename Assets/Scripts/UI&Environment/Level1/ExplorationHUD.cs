using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ExplorationHUD : MonoBehaviour
{
    GameObject remnant1, remnant2;
    List<GameObject> remList = new List<GameObject>();
    int remnantCount;
    public static ExplorationHUD expHUD;
    private void Start()
    {
        expHUD = this;//singleton for the scene
        remnant1 = transform.Find("r1").gameObject;
        remnant2 = transform.Find("r2").gameObject;
        remnant1.SetActive(false);       
        remnant2.SetActive(false);
        remList.Clear();
        remList.Add(remnant1);
        remList.Add(remnant2);
        //remnantCount = 1;
        remnantCount = 2;//<------- 2 once remnant 2 is set up with scens index 1 and new description properly
        Invoke("uiUpdate", 0.2f);
    }

    public void uiUpdate()
    {
        //Scene index alr set on each remnant
        for (int x = 0; x < remnantCount; x++)
        {
            string currentRemnantName = PlayerCommonStatus.getRemnantNameWithSceneIndex(x);
            if (PlayerCommonStatus.checkIfRemnantExist(currentRemnantName))
            {
                remList[x].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = PlayerCommonStatus.getRemnantDescriptionWithSceneIndex(x);
                remList[x].GetComponent<Image>().sprite = PlayerCommonStatus.getRemnantSpriteWithSceneIndex(x);

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
