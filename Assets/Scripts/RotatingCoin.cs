using UnityEngine;

public class RotatingCoin : MonoBehaviour
{
   [SerializeField] private float rotationSpeed = 100f;

   void Update()
   {
       transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
   }
}
