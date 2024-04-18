using UnityEngine;
using UnityEngine.SceneManagement;

public class VerySimpleUIHandler : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _loseMenu;
    [SerializeField] GameObject _tutorial;

    [Header("Music")]
    [SerializeField] AudioClip _buttonClick;
    [SerializeField] AudioSource _battleAudio;

    private AudioSource _audioSource;
    private PlayerAction _player;

    private void Awake()
    {
        Time.timeScale = 0;
        _mainMenu.SetActive(true);
        _player = FindObjectOfType<PlayerAction>();
        _audioSource = GetComponent<AudioSource>();

        _player.TryGetComponent(out VitalitySystem vitalitySystem);
        vitalitySystem.OnDeath += LoseState;

        _battleAudio.Play();
        _battleAudio.Pause();

        _audioSource.Play();
    }
    public void ClickPlayButton()
    {
        PlayBattleMusic();
        _audioSource.PlayOneShot(_buttonClick);
        _mainMenu.SetActive(false);
        _pauseButton.SetActive(true);
        _tutorial.SetActive(true);
        Time.timeScale = 1;
    }
    public void ClickPauseButton()
    {
        PlayMenuMusic();
        _audioSource.PlayOneShot(_buttonClick);
        _pauseButton.SetActive(false);
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClickResumeButton()
    {
        PlayBattleMusic();
        _audioSource.PlayOneShot(_buttonClick);
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    public void LoseState()
    {
        Time.timeScale = 0;
        _loseMenu.SetActive(true);
    }
    public void Restart()
    {
        _audioSource.PlayOneShot(_buttonClick);
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        _audioSource.PlayOneShot(_buttonClick);
        Application.Quit();
    }
    private void PlayBattleMusic()
    {
        _audioSource.Stop();
        _battleAudio.UnPause();
    }
    private void PlayMenuMusic()
    {
        _battleAudio.Pause();
        _audioSource.Play();
    }
}
