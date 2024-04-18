using System.Collections;
using System.Security.Cryptography;

using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public readonly string LeftArrow = "LeftArrowWait", RightArrow = "RightArrowWait";

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Animator _leftArrowAnim;
    [SerializeField] private Animator _rightArrowAnim;

    private bool RightArrowClick => Input.GetKeyDown(KeyCode.RightArrow);
    private bool LeftArrowClick => Input.GetKeyDown(KeyCode.LeftArrow);

    private bool _leftClick, _rightClick;

    private void OnEnable()
    {
        StartCoroutine(WaitRightClick());
        StartCoroutine(WaitLeftClick());
        StartCoroutine(WaitFinishTutorial());
    }
    public IEnumerator WaitLeftClick()
    {
        yield return new WaitUntil(() => LeftArrowClick);
        _leftArrowAnim.SetTrigger("LeftArrow");
        yield return _leftClick = true;
    }
    public IEnumerator WaitRightClick()
    {
        yield return new WaitUntil(() => RightArrowClick);
        _rightArrowAnim.SetTrigger("RightArrow");
        yield return _rightClick = true;
    }
    public IEnumerator WaitFinishTutorial()
    {
        yield return new WaitUntil(() => _leftClick && _rightClick);
        _enemySpawner.gameObject.SetActive(true);
    }
}