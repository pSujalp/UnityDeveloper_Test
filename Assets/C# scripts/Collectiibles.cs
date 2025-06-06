using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Collectiible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static int collectibleCount = 0;

    private static int totalCollectibles = 0;

    bool ToBeTriggeredOnce = true;

    [SerializeField] TextMeshProUGUI textMeshPro;

    [SerializeField] GameManager gameManager;

    void Start()
    {
        collectibleCount=0;
        OnValidate();
        Debug.Log("Total collectibles: " + totalCollectibles);
        textMeshPro.text = "Collectibles: " + collectibleCount + "/" + totalCollectibles;
        if (gameManager == null)
        {
            
            if (gameManager == null)
            {
            Debug.LogError("GameManager not found in the scene!");
            }
        }
        
    }
 
    void OnValidate()
    {
        totalCollectibles = 0;
        Collectiible[] collectibles = FindObjectsOfType<Collectiible>();
        totalCollectibles = collectibles.Length;
    }
    // Update is called once per frame

    public void LevelRe()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(ToBeTriggeredOnce){++collectibleCount;ToBeTriggeredOnce = false;}
            else { return; }
            if(collectibleCount>=totalCollectibles)
            {
                Debug.Log("All collectibles collected!");
                textMeshPro.text = "All collectibles collected! Level Restarting in 5 seconds";
                gameManager.StopTimer();
                textMeshPro.color = Color.green;
                Invoke(nameof(LevelRe),5f);
                gameManager.StartCoroutine(gameManager.StopTimer());
                textMeshPro.color = Color.green;
            }
            else
            {
                
                ToBeTriggeredOnce = false;
                textMeshPro.text = "Collectibles: " + collectibleCount + "/" + totalCollectibles;
                Debug.Log("Collectible collected! Total: " + collectibleCount + "/" + totalCollectibles);
                
               
            }
            Destroy(gameObject);
        }
    }
}

