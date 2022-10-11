using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{
    public Image bar;
    public TMPro.TMP_Text percent;
    public float start = 0;
    public float end = 100;
    [SerializeField] Image progressBar;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene");
        op.allowSceneActivation = false;
        float timer = 0.0f;
        StartCoroutine(loading());
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                
                if (progressBar.fillAmount == 1.0f && start >=100)
                {                    
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        
    }

    IEnumerator loading()
    {
        while (start < 100)
        {
            bar.fillAmount = start / end;
            start += 1;
            percent.text = start.ToString() + "%";
            yield return new WaitForSeconds(0.03f);
        }
    }
}
