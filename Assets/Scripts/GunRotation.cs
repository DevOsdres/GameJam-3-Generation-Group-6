using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f; 
    [SerializeField] private float minYAngle = -140f;  
    [SerializeField] private float maxYAngle = -20f;   
    [SerializeField] private float minXAngle = -60f;  
    [SerializeField] private float maxXAngle = 60f;
    [SerializeField] private float fixedXAngle = -120f; 

    [SerializeField] private Transform pitchObject; // The empty object whose X rotation will be controlled

    private void Start()
    {
        if (pitchObject != null)
        {
            // Initialize the rotation of the gun to the fixedXAngle on X-axis and the current Y-axis rotation
            transform.rotation = Quaternion.Euler(fixedXAngle, transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            Debug.LogError("PitchObject is not assigned.");
        }
    }

    void Update()
    {
        if (pitchObject == null)
        {
            Debug.LogError("PitchObject is not assigned.");
            return;
        }

        // Get mouse input
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        // Normalize the mouse input to get a value between -1 and 1
        float normalizedMouseX = (mouseX / Screen.width) * 2 - 1;
        float normalizedMouseY = (mouseY / Screen.height) * 2 - 1;

        // Calculate the target Y rotation based on the mouse input
        float targetRotationY = Mathf.Lerp(minYAngle, maxYAngle, (normalizedMouseX + 1) / 2);

        // Calculate the target X rotation for the pitchObject based on the mouse input
        float targetRotationX = Mathf.Lerp(minXAngle, maxXAngle, (normalizedMouseY + 1) / 2);

        // Create the target rotation for the gun
        Quaternion targetGunRotation = Quaternion.Euler(fixedXAngle, targetRotationY, 0f);

        // Smoothly interpolate to the target rotation for the gun
        transform.rotation = Quaternion.Slerp(transform.rotation, targetGunRotation, rotationSpeed * Time.deltaTime);

        // Smoothly interpolate the pitchObject's rotation
        Quaternion targetPitchRotation = Quaternion.Euler(targetRotationX, pitchObject.rotation.eulerAngles.y, pitchObject.rotation.eulerAngles.z);
        pitchObject.rotation = Quaternion.Slerp(pitchObject.rotation, targetPitchRotation, rotationSpeed * Time.deltaTime);
    }
}
