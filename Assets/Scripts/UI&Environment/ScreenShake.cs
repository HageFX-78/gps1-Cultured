using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    /*Attach this script to cameras only, then when wan to call it just refer to this on the cam and the last function
     * 
     * 
     * This can be used on other game objects as well to make them shake like the enemy, jsut need to attach to them instead
     */
    [SerializeField] float shakeDuration = 0f;
    [SerializeField] float shakeMagnitude = 0.3f;
    [SerializeField] float dampingSpeed = 1.0f;
    public RectTransform uiRef;
    public bool isShaking;
    public bool isFollowCam;//On if we using it in exploration phase
    Vector3 currentPos;

    void Start()
    {
        isShaking = false;
        currentPos = transform.localPosition;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isFollowCam && !isShaking)
        {
            currentPos = transform.localPosition;
        }


        if (shakeDuration > 0)
        {
            isShaking = true;
            

            transform.localPosition = currentPos + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            isShaking = false;
            if (!isFollowCam)
            {

                transform.localPosition = currentPos;

            }

        }
    }
    public void ShakeScreen(float shakeTime, float shakeStrength = 0.3f)
    {
        shakeDuration = shakeTime;
        shakeMagnitude = shakeStrength;
    }
}
