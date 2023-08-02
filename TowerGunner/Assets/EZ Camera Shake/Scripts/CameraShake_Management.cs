using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class CameraShake_Management : MonoBehaviour
{
    [Range(0.0f,10.0f)]
    [SerializeField] float magnitude;
    [Range(0.0f,20.0f)]
    [SerializeField] float roughness;
    [Range(0.0f, 10.0f)]
    [SerializeField] float fadeInTime;
    [Range(0.0f, 10.0f)]
    [SerializeField] float fadeOutTime;

    // Start is called before the first frame update
    public void ShakeCamera() {

        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
