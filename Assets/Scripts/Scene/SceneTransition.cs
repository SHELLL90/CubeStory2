using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text textPercent;
    [SerializeField] private Image imageProgressBar;
    [SerializeField] private Image background;

    private Animator _animator;

    public static SceneTransition instance;
    public static string lastSceneName;

    private AsyncOperation _loadingSceneOperation;

    private static bool _sceneIsSwitching;

    private static bool _showAndroidAd;

    public static bool SceneIsSwitching
    {
        get { return _sceneIsSwitching; }
        private set { _sceneIsSwitching = value; }
    }

    private static int _numberOfSwitches = 0;

    public static Action ActionSceneSwitching { get; set; }

    public static bool NeedWaitEndAnimation;

    private static float _maxValueProgress = 1.0f;

    public static void SwitchScene(string nameScene)
    {
        NeedWaitEndAnimation = false;
        _maxValueProgress = 0.95f;

        if (SceneIsSwitching) return;
        ActionSceneSwitching?.Invoke();

        lastSceneName = SceneManager.GetActiveScene().name;

        Time.timeScale = 1.0f;
        SceneIsSwitching = true;

        instance._animator.SetTrigger("Start");

        instance._loadingSceneOperation = SceneManager.LoadSceneAsync(nameScene);
        instance._loadingSceneOperation.allowSceneActivation = false;

        _numberOfSwitches++;
        if (_numberOfSwitches % 3 == 0) _showAndroidAd = true;
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        SceneManager.activeSceneChanged += Initialization;

        _animator = GetComponent<Animator>();

        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (!SceneIsSwitching) return;

        float progress = _loadingSceneOperation.progress;
        progress = Mathf.Clamp(progress, 0, _maxValueProgress);

        textPercent.text = Mathf.RoundToInt(progress * 100).ToString() + "%";

        imageProgressBar.fillAmount = progress;
    }
    void Initialization(Scene current, Scene next)
    {
        StartCoroutine(WaitAnimatorEnd());
    }

    private IEnumerator WaitAnimatorEnd()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        float timeEnd = Time.realtimeSinceStartup + 10.0f;
        Debug.Log("Time End " + timeEnd);
        while (NeedWaitEndAnimation && timeEnd > Time.realtimeSinceStartup)
        {
            yield return null;
        }

        _maxValueProgress = 1.0f;

        instance._animator.SetTrigger("End");
    }

    public void ActionCloseTransition()
    {
        background.raycastTarget = false;
        SceneIsSwitching = false;
#if UNITY_WEBGL
        AdsManager.Instance.ShowFullScreenAd();
#endif

#if UNITY_ANDROID
        if (_showAndroidAd)
        {
            //AdsManager.Instance.ShowFullScreenAd();
            _showAndroidAd = false;
        }
#endif
    }
    public void AnimationLoadingStart()
    {
        background.raycastTarget = true;
    }
    public void AnimationLoadingEnd()
    {
        StopAllCoroutines();
        StartCoroutine(WaitSceneActivation());
    }
    private IEnumerator WaitSceneActivation()
    {
        yield return new WaitForSeconds(0.3f);
        _loadingSceneOperation.allowSceneActivation = true;
    }
}
