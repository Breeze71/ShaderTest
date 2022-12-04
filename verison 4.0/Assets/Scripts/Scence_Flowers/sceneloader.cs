using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
    public void OnLoginButtonClick()
    {
        SceneManager.LoadScene(1); // 1
    }
}
