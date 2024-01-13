using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private UnderwaterAudioEffect audioEffect;

    private void Start()
    {
        canvas.enabled = false;
    }

    public void ToggleCanvas()
    {
        canvas.enabled = true;
        audioEffect.SetUnderwaterEffect(true);

        Time.timeScale = 0;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;

        audioEffect.SetUnderwaterEffect(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

