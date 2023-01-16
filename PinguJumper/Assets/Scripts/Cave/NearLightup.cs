using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NearLightup : MonoBehaviour
{
    public bool enableParticleSystem = true;
    private Light lig;
    public float distance = 30f;
    [Range(0f,1f)]
    public float intensityFlicker = 0.2f;
    private float startIntensity;
    private int frameSkip = 100;
    private float targetIntesity = 1;
    [SerializeField]private ParticleSystem effect;
    

    private void Start()
    {
        lig = GetComponent<Light>();
        startIntensity = lig.intensity;
        if(!enableParticleSystem)
            effect.Stop();
    }

    public void Update()
    {
        if (frameSkip++ > 20)
        {
            frameSkip = 0;
            GameObject player = GameObject.FindObjectOfType<PlayerBehavior>().gameObject;
            if (Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                if(effect.isStopped && enableParticleSystem)
                    effect.Play();
                
                targetIntesity =
                    UnityEngine.Random.Range(startIntensity - intensityFlicker, startIntensity + intensityFlicker);
            }
            else
            {
                targetIntesity = 0;
                if(effect.isPlaying  && enableParticleSystem)
                    effect.Stop();
            }
        }
        lig.intensity = Mathf.Lerp(lig.intensity, targetIntesity, 0.3f);
    }
}
