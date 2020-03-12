using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the object should shake for.
    public static float shakeDuration = 0;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.5f;
    public float decreaseFactor = 1.5f;

    Vector3 originalPos;
    float now,incNow;

    void Start()
    {
        now = 0;
        incNow = 0;
        
    }

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        
        if (shakeDuration > 0)
        {
            //camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            transform.localPosition = originalPos + new Vector3(0
                ,0, Random.insideUnitSphere.z * shakeAmount);

            shakeDuration -= Time.deltaTime * decreaseFactor;
            
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }

   


}
