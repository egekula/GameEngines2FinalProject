using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("NextLevel");
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        int nextBuildIndex = currentSceneIndex + 1;
        if (nextBuildIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextBuildIndex = 0;
        }
        
        SceneManager.LoadScene(nextBuildIndex);
    }
}
