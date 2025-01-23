using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button predictiveModeButton;
    [SerializeField] private Color32 predictiveColor;
    [SerializeField] private Color32 nonPredictiveColor;
    private void Start() 
    {
        ChangeButtonColor();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void PredictiveMode()
    {
        BallBehaviour.predictiveMode = !BallBehaviour.predictiveMode;
        ChangeButtonColor();
    }

    private void ChangeButtonColor()
    {
        if (BallBehaviour.predictiveMode)
        {
            predictiveModeButton.image.color = predictiveColor;
        }
        else
        {
            predictiveModeButton.image.color = nonPredictiveColor;
        }
    }
}
