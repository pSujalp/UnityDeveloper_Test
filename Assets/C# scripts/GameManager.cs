using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int time = 45;

    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] GameObject collectibles;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(WaitAndEnd());
    }

    IEnumerator WaitAndEnd()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;

            
            Debug.Log("Time remaining: " + time);
            timerText.text = "Time: " + time.ToString();
        }
        timerText.text = "Time's UP! RESTART in 5 seconds...";
        timerText.color=Color.red;
        Destroy(collectibles.gameObject);
        Cursor.visible = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
