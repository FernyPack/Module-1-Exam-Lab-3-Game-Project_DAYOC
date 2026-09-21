using UnityEngine;

public class ExitZone : MonoBehaviour

{
    public GameObject winUI;
    public GameObject GameplayUI;
    public AudioSource ambientAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.objectiveText.text = "Objective Complete!";

            
            winUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;
        }
        if (ambientAudio != null)
        {
            ambientAudio.Stop();
        }
    }
}