using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    // Public method to be called when the quit button is clicked
    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked."); // Add debug log
        // Load the Help Screen scene
        SceneManager.LoadScene("MainMenu");
    }
}
