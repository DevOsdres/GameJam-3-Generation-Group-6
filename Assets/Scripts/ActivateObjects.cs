using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    private GameObject currentlyActiveObject;

    private void Start()
    {
        // Ensure object1 is active by default
        SetActiveObject(object1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveObject(object1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveObject(object2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveObject(object3);
        }
    }

    private void SetActiveObject(GameObject obj)
    {
        // Deactivate the currently active object if it's different
        if (currentlyActiveObject != obj)
        {
            if (currentlyActiveObject != null)
            {
                currentlyActiveObject.SetActive(false);
            }

            obj.SetActive(true);
            currentlyActiveObject = obj;
        }
    }
}
