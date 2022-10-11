using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool stop;
    public float stopTime;
    public Transform ShakeCam;
    public Vector3 Shake;

    public void StopTime()
    {
        if (!stop)
        {
            stop = true;
            ShakeCam.localPosition = Shake;
            Time.timeScale = 0.0f;

            StartCoroutine("ReturnTimeScale");
        }
    }

    IEnumerator ReturnTimeScale()
    {
        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = 1;
        ShakeCam.localPosition = Vector3.zero;
        stop = false;
    }
}
