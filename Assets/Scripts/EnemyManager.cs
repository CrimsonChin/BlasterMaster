using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int EnemyCount { get; set; }

    public void HandleDeath()
    {
        EnemyCount--;

        if (EnemyCount > 0)
        {
            return;
        }

        var audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayWin();

        // give the win sound time to play
        Invoke("ChangeLevel", 3f);
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("Win");
    }
}
