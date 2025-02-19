using UnityEngine;

public class CoinCollector : MonoBehaviour
{
   private int score = 0;

   void OnTriggerEnter(Collider other)
    {
        Transform rootObject = other.transform.root;
        if (rootObject.CompareTag("Player"))
        {
            score += 1;
            Debug.Log("Score: " + score);

            Destroy(gameObject);
        }
    }
}