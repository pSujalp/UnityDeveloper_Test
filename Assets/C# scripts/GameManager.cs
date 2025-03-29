using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : MonoBehaviour
{
    [SerializeField] int time = 45;

    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] GameObject collectibles;

    private CancellationTokenSource cancellationTokenSource;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cancellationTokenSource = new CancellationTokenSource();
        StartCoroutine(WaitAndEnd(cancellationTokenSource.Token));
    }

    public IEnumerator StopTimer()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        timerText.text = "All collectibles collected!";
        timerText.color = Color.green;
        yield return new WaitForSeconds(5f);
        timerText.text = "Level Restarting...";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitAndEnd(CancellationToken token)
    {
        while (time > 0)
        {
            if (token.IsCancellationRequested)
            {
                yield break;
            }

            yield return new WaitForSeconds(1);
            time--;

            Debug.Log("Time remaining: " + time);
            timerText.text = "Time: " + time.ToString();
        }

        timerText.text = "Time's UP! RESTART in 5 seconds...";
        timerText.color = Color.red;
        Destroy(collectibles.gameObject);
        Cursor.visible = true;

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
