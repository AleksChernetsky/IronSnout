using UnityEngine;
using UnityEngine.SceneManagement;

public class VerySimpleUIHandler : MonoBehaviour
{
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _loseMenu;

    private PlayerAction _player;

    private void Awake()
    {       
        Time.timeScale = 0;
        _mainMenu.SetActive(true);
        _player = FindObjectOfType<PlayerAction>();

        _player.TryGetComponent(out VitalitySystem vitalitySystem);
        vitalitySystem.OnDeath += LoseWindow;
    }
    public void Play()
    {
        _mainMenu.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        _pauseButton.SetActive(false);
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    public void LoseWindow()
    {
        Time.timeScale = 0;
        _loseMenu.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
