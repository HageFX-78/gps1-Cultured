using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class SanityUIController : MonoBehaviour
{
    [SerializeField] Volume globalVol;//Remember to reference 
    [SerializeField] float sanityIncrementValue;
    [SerializeField] float sanityIncrementWaitTime;
    Vignette vg;
    Bloom bl;
    FilmGrain fg;

    // Start is called before the first frame update
    void Start()
    {

        globalVol.profile.TryGet(out vg);
        globalVol.profile.TryGet(out bl);
        globalVol.profile.TryGet(out fg);
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
        }
        else if(curSanity >= 80)
        {
            vg.color.value = new Color(2, 0, 0);
            //vg.intensity.value = 0.6f;
            StartCoroutine(pulsatingSanity(0.4f, 0.6f));
            bl.intensity.value = 2.5f;
            fg.intensity.value = 0.5f;
        }
        else if (curSanity >= 60)
        {
            vg.color.value = new Color(6, 0, 0);
            //vg.intensity.value = 0.8f;
            StartCoroutine(pulsatingSanity(0.5f, 0.7f));
            bl.intensity.value = 3f;
            fg.intensity.value = 0.7f;
        }
        else if (curSanity >= 40)
        {
            vg.color.value = new Color(10, 0, 0);
            //vg.intensity.value = 0.9f;
            StartCoroutine(pulsatingSanity(0.7f, 0.9f));
            bl.intensity.value = 4f;
            fg.intensity.value = 0.85f;
        }
        else if (curSanity >= 20)
        {
            vg.color.value = new Color(15, 0, 0);
            //vg.intensity.value = 1.0f;
            StartCoroutine(pulsatingSanity(0.8f, 1.0f));
            bl.intensity.value = 5f;
            fg.intensity.value = 1.0f;
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
