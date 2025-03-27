        using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;  
    public float sensitivity = 200f; 
   
    public float minY = -30f, maxY = 60f; 

    private float yaw = 0f; 
    private float pitch = 0f; 

  
    public float distance = 5f; 
    public float height = 2f; 
    public float smoothSpeed = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position - (rotation * Vector3.forward * distance);
        transform.LookAt(target.position);
    }

     void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 targetPosition = target.position - target.forward * distance + Vector3.up * height;

        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
    }
}
