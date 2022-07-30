using UnityEngine;

public class Farm : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.instance.blade.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.instance.blade.SetActive(false);
        }
    }
}