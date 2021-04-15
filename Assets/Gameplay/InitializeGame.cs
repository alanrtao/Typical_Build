using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeGame : MonoBehaviour
{
    public AnimationCurve fade;

    // kickstart game initialization
    public void Initialize()
    {
        GetComponent<UnityEngine.UI.Button>().enabled = false;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        UnityEngine.UI.Image crossfade = transform.parent.GetChild(0)
            .GetComponent<UnityEngine.UI.Image>();
        float t = 0;
        fade.postWrapMode = WrapMode.Clamp;
        while (t < 1)
        {
            t += Time.deltaTime;
            crossfade.color = new Color(0, 0, 0, fade.Evaluate(t));
            yield return null;
        }
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        ScriptDispenser.Load();
        GetComponent<FMODUnity.StudioListener>().enabled = true; // this kickstarts FMOD bank loading
        AsyncOperation load = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!load.isDone)
        {
            yield return null;
        }
    }
}
