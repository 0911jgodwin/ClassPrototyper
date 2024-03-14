using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastBar : MonoBehaviour
{
    [SerializeField]
    private Image castBar;
    [SerializeField]
    private TMP_Text castName;
    [SerializeField]
    private Image castIcon;
    [SerializeField]
    private TMP_Text castTime;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Sprite sprite;

    private Coroutine castRoutine;
    private Coroutine fadeRoutine;

    private bool castFinished;
    public bool activelyCasting = false;

    public delegate void CastTick(CastStatus castStatus);
    public static event CastTick OnTick;

    public void CastSpell(string spellName, Sprite spellIcon, float spellTime)
    {
        activelyCasting = true;
        castName.text = spellName;
        castIcon.sprite = spellIcon;

        castBar.fillAmount = 0;

        castFinished = false;
        castRoutine = StartCoroutine(CastProgress(spellTime));
        fadeRoutine = StartCoroutine(FadeBar());
    }

    public void ChannelSpell(string spellName, Sprite spellIcon, float spellTime, float tickRate)
    {
        activelyCasting = true;
        castName.text = spellName;
        castIcon.sprite = spellIcon;

        castBar.fillAmount = 1;

        castFinished = false;
        castRoutine = StartCoroutine(ChannelProgress(spellTime, tickRate));
        fadeRoutine = StartCoroutine(FadeBar());
    }

    private IEnumerator ChannelProgress(float spellTime, float tickRate)
    {
        float timePassed = Time.deltaTime;
        float nextTick = 0.0f;

        float rate = 1.0f / spellTime;

        float progress = 1.0f;
        castFinished = true;
        while (progress >= 0f)
        {
            castBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress -= rate * Time.deltaTime;
            timePassed += Time.deltaTime;

            if (timePassed > nextTick)
            {
                nextTick = timePassed + tickRate;
                if (OnTick != null)
                    OnTick(CastStatus.TICK);
                //Debug.Log("Tick at " + Time.deltaTime);
            }

            castTime.text = (spellTime - timePassed).ToString("F2");

            if (spellTime - timePassed < 0)
            {
                castTime.text = "0.0";
            }
            yield return null;
        }
        //Debug.Log("Tick at " + Time.deltaTime);
        if (OnTick != null)
            OnTick(CastStatus.TICK);
        StopCasting();
    }

    private IEnumerator CastProgress(float spellTime)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / spellTime;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;

            castTime.text = (spellTime - timePassed).ToString("F2");

            if (spellTime - timePassed < 0)
            {
                castTime.text = "0.0";
            }
            yield return null;
        }
        if (OnTick != null)
            OnTick(CastStatus.TICK);
        castFinished = true;
        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.25f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        activelyCasting = false;
        if (!castFinished)
        {
            OnTick(CastStatus.FAILURE);
        }
        else
        {
            OnTick(CastStatus.SUCCESS);
        }
        OnTick = delegate { };
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
        }
        if (castRoutine != null)
        {
            StopCoroutine(castRoutine);
            castRoutine = null;
        }
    }
}
