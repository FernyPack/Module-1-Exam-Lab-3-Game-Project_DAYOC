using UnityEngine;

public class Trash : MonoBehaviour
{
    bool playerNearby = false;

    public AudioClip Pickup;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (Pickup != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(
                    Pickup,
                    Camera.main.transform.position,
                    0.015f
                );
            }

            GameManager.Instance.CollectTrash();
            Destroy(gameObject);

            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}