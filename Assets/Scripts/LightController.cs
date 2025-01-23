using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    private bool mustDisable;
    public IEnumerator LightFlash(Light2D light, float flashDuration, float flashIntensity)
    {
        if (light.enabled == false)
        {
            mustDisable = true;
            light.enabled = true;
        }
        
        light.intensity *= 3;
        yield return new WaitForSeconds(flashDuration);
        light.intensity /= 3;
        
        if (mustDisable)
        {
            light.enabled = false;
        }
    }
}
