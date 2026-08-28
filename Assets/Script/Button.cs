using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{  
    [SerializeField] private TMP_Text Score;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowScore(int i)
    {
        Score.text = $"Score : {i}";
    }

    public void Setactive()
    {
        this.gameObject.SetActive(true);
    }

    public void start()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
