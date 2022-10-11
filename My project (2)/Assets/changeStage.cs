using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeStage : MonoBehaviour
{
    private void Update()
    {
        
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Loading");
    }
    
}
