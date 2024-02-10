using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour
{
    // Public method to be called when the help button is clicked
    public void OnHelpButtonClicked()
    {
        // Load the Help Screen scene
        SceneManager.LoadScene("Help");
    }
}
