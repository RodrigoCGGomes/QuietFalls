using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanel_MainTitle : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}