using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine("ReloadScene");       
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
