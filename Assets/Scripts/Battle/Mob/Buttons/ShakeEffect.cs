using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0f;
    [SerializeField] float shakeMagnitude = 0.3f;
    [SerializeField] float dampingSpeed = 1.0f;
    public bool isShaking;
    Vector3 currentPos;

    void Start()
    {
        isShaking = false;
        currentPos = transform.localPosition;

    }
    // Update is called once per frame
    void Update()
    {
        if (!isShaking)
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
            transform.localPosition = currentPos;

        }
    }
    public void ShakeScreen(float shakeTime, float shakeStrength = 0.3f)
    {
        shakeDuration = shakeTime;
        shakeMagnitude = shakeStrength;
    }
}
