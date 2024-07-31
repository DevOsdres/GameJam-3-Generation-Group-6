using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource hoverSound;
    public AudioSource clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter: " + gameObject.name);
        if (hoverSound != null && hoverSound.isActiveAndEnabled)
        {
            hoverSound.Play();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click: " + gameObject.name);
        if (clickSound != null && clickSound.isActiveAndEnabled)
        {
            clickSound.Play();
            Debug.Log("Click sound played");
        }
    }
}