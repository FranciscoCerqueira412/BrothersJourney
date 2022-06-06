using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsDown : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D[] lgd;
    // Start is called before the first frame update

    public void Start()
    {
        StartCoroutine(time_blink());
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator time_blink()
    {
        for (; ;)
        {
            foreach (var sr in lgd)
            {
                sr.enabled = false;
                yield return new WaitForSeconds(0.1f);
                sr.enabled = true;
                yield return new WaitForSeconds(0.1f);
                yield return new WaitForSeconds(6f);
            }
        }
       
        
        
    }
}
