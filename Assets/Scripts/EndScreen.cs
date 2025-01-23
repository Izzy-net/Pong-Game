using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI conditionText;
    [SerializeField] GameObject ball;

    private void Start() 
    {
        conditionText.text = WinLose.condition;
        if (conditionText.text == "Lose")
        {
            ball.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            ball.GetComponentInChildren<Light2D>().color = Color.red;
            ball.GetComponentInChildren<Light2D>().intensity /= 2;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
