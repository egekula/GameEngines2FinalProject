using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    public int score = 0;


    private void Awake()
    {
        int findPlayerStateManager = FindObjectsOfType<PlayerStateManager>().Length;
        if (findPlayerStateManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ProcessOfPlayerDeath()
    {
        if (playerLives>1)
        {
            StartCoroutine("TakeDeathForTime");
        }
        else
        {
            StartCoroutine("ResetGameSessionForTime");
        }
    }
    private void TakeDeath()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void TakeCoin(int points)
    {
        score += points;
    }
    IEnumerator TakeDeathForTime()
    {
        yield return new WaitForSeconds(1f);
        TakeDeath();
    }

    IEnumerator ResetGameSessionForTime()
    {
        yield return new WaitForSeconds(1f);
        ResetGameSession();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
