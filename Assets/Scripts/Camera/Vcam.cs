using UnityEngine;
using Cinemachine;

public class Vcam : MonoBehaviour
{
    public GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        
        virtualCam.SetActive(true);

        //How can I make it that I do not have to initialize the player at runtime?
        virtualCam.GetComponent<CinemachineVirtualCamera>().Follow = other.transform;
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            virtualCam.SetActive(false);
        }
    }
}
