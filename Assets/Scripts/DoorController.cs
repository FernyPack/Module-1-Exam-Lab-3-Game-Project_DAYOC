using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openHeight = 4f;
    public float openSpeed = 2f;
    public AudioSource audioSource;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool opening = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        if (opening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                openSpeed * Time.deltaTime
            );
        }
    }

    public void OpenDoor()
    {
        opening = true;

        if (audioSource != null)
        {
            audioSource.volume = 0.15f;
            audioSource.Play();
        }
    }
}