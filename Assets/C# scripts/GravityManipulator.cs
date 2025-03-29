using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityManipulator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f; 
    private Vector3 gravityDirection ; 

    void Start()
    {
        gravityDirection = Physics.gravity.normalized;
    }

    void Update()
    {
        Quaternion targetRotation = transform.rotation;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation *= Quaternion.Euler(0, 0, 90); 
            gravityDirection = -transform.right; 
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation *= Quaternion.Euler(0, 0, -90); 
            gravityDirection = transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetRotation *= Quaternion.Euler(90, 0, 0); 
            gravityDirection = transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetRotation *= Quaternion.Euler(-90, 0, 0); 
            gravityDirection = -transform.forward;
        }
        
        transform.rotation = targetRotation;
        
        
    }
}