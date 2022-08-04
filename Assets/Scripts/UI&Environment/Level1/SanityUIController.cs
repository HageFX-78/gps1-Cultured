using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class SanityUIController : MonoBehaviour
{
    [SerializeField] Volume globalVol;//Remember to reference 
    [SerializeField] float sanityIncrementValue;
    [SerializeField] float sanityIncrementWaitTime;
    Vignette vg;
    Bloom bl;
    FilmGrain fg;

    Image heartUI;

    // Start is called before the first frame update
    void Start()
    {

        globalVol.profile.TryGet(out vg);
        globalVol.profile.TryGet(out bl);
        globalVol.profile.TryGet(out fg);

        heartUI = transform.GetComponentInChildren<Image>();
        updateSanity();
    }

    // Update is called once per frame
    void updateSanity()
    {
        float curSanity = PlayerCommonStatus.getSanity();
        
        if(curSanity >=100)
        {
            vg.color.value = new Color(0, 0, 0);
            vg.intensity.value = 0.5f;
            bl.intensity.value = 2f;
            fg.intensity.value = 0.3f;

            heartUI.color = new Color32(252,40,3, 255);
        }
        else if(curSanity >= 80)
        {
            vg.color.value = new Color(2, 0, 0);
            //vg.intensity.value = 0.6f;
            StartCoroutine(pulsatingSanity(0.4f, 0.6f));
            bl.intensity.value = 2.5f;
            fg.intensity.value = 0.5f;

            heartUI.color = new Color32(252, 98, 3, 255);
        }
        else if (curSanity >= 60)
        {
            vg.color.value = new Color(6, 0, 0);
            //vg.intensity.value = 0.8f;
            StartCoroutine(pulsatingSanity(0.5f, 0.7f));
            bl.intensity.value = 3f;
            fg.intensity.value = 0.7f;

            heartUI.color = new Color32(252, 194, 3, 255);
        }
        else if (curSanity >= 40)
        {
            vg.color.value = new Color(10, 0, 0);
            //vg.intensity.value = 0.9f;
            StartCoroutine(pulsatingSanity(0.7f, 0.9f));
            bl.intensity.value = 4f;
            fg.intensity.value = 0.85f;

            heartUI.color = new Color32(132, 3, 252, 255);
        }
        else if (curSanity >= 20)
        {
            vg.color.value = new Color(15, 0, 0);
            //vg.intensity.value = 1.0f;
            StartCoroutine(pulsatingSanity(0.8f, 1.0f));
            bl.intensity.value = 5f;
            fg.intensity.value = 1.0f;

            heartUI.color = new Color32(91, 90, 92, 255);
        }
    }
    IEnumerator pulsatingSanity(float min, float max)
    {
        float vgIntensity;
        float plusVal = sanityIncrementValue;
        while(true)
        {
            vgIntensity = vg.intensity.value;
            if (vgIntensity<=min)
            {
                plusVal = sanityIncrementValue;
            }
            else if(vgIntensity >= max)
            {
                plusVal = -sanityIncrementValue;
            }

            vg.intensity.value = vgIntensity+ plusVal;

            yield return new WaitForSecondsRealtime(sanityIncrementWaitTime);
        }
    }
}
