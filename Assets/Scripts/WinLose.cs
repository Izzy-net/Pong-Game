using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public static string condition;
    public void HandleWinLose(string tag)
    {
        condition = tag;
        SceneManager.LoadScene("EndScreen");
    }
}
