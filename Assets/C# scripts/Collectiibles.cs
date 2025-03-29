using UnityEngine;

public class Collectiible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] static int collectibleCount = 0;

    [SerializeField]  static int totalCollectibles = 0;

    void Start()
    {
        OnValidate();
        Debug.Log("Total collectibles: " + totalCollectibles);
    }
    void OnValidate()
    {
        totalCollectibles = 0;
        Collectiible[] collectibles = FindObjectsOfType<Collectiible>();
        totalCollectibles = collectibles.Length;
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ++collectibleCount;
            if(collectibleCount>=totalCollectibles)
            {
                Debug.Log("All collectibles collected!");
            }
            else
            {
                Debug.Log("Collectible collected! Total: " + collectibleCount + "/" + totalCollectibles);
            }
            Destroy(gameObject);
        }
    }
}
