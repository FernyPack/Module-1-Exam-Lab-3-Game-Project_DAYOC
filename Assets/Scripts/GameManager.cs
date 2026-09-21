using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text objectiveText;
    public TMP_Text trashText;

    public GameObject gameplayUI;
    public GameObject gameOverUI;
    public GameObject winUI;

    [Header("Game")]
    public int totalTrash = 5;
    private int collectedTrash = 0;

    public GameObject exitDoor;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateUI();
        objectiveText.text = "Objective: Collect all trash";

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (winUI != null)
            winUI.SetActive(false);
    }

    public void CollectTrash()
    {
        collectedTrash++;

        UpdateUI();

        if (collectedTrash >= totalTrash)
        {
            exitDoor.GetComponent<DoorController>().OpenDoor();
            objectiveText.text = "Objective: Escape the warehouse";
        }
    }

    void UpdateUI()
    {
        if (trashText != null)
            trashText.text = "Trash: " + collectedTrash + " / " + totalTrash;
    }

    public void GameOver()
    {
        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Win()
    {
        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        if (winUI != null)
            winUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}