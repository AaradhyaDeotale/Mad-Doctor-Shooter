using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeIcon : MonoBehaviour
{
    public void OnHomeIconClick()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
