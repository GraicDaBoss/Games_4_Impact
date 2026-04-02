using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cameras")]
    public Camera gameplayCamera;
    public Camera cutsceneCamera;

    [Header("UI")]
    public GameObject gameplayUI;
    public GameObject introPanel;
    public TMP_Text introText;
    public Button beginButton;

    [Header("Settings")]
    [TextArea] public string narrativeText = "Your text here...";
    public float zoomTargetFOV = 30f;
    public float zoomDuration = 2f;
    public float holdDuration = 1.5f;
    public string playerTag = "Player";

    private float originalFOV;
    private bool triggered = false;

    void Start()
    {
        originalFOV = cutsceneCamera.fieldOfView;
        cutsceneCamera.gameObject.SetActive(false);
        introPanel.SetActive(false);
        beginButton.gameObject.SetActive(false);
        beginButton.onClick.AddListener(OnBeginClicked);
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag(playerTag)) return;
        triggered = true;
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        gameplayUI.SetActive(false);
        introPanel.SetActive(true);
        introText.text = narrativeText;

        gameplayCamera.gameObject.SetActive(false);
        cutsceneCamera.gameObject.SetActive(true);
        cutsceneCamera.fieldOfView = originalFOV;

        yield return StartCoroutine(ZoomCamera(zoomTargetFOV, zoomDuration));
        yield return new WaitForSeconds(holdDuration);

        beginButton.gameObject.SetActive(true);
    }

    void OnBeginClicked()
    {
        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene()
    {
        yield return StartCoroutine(ZoomCamera(originalFOV, zoomDuration));

        introPanel.SetActive(false);
        cutsceneCamera.gameObject.SetActive(false);
        gameplayCamera.gameObject.SetActive(true);
        gameplayUI.SetActive(true);
    }

    IEnumerator ZoomCamera(float targetFOV, float duration)
    {
        float start = cutsceneCamera.fieldOfView;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cutsceneCamera.fieldOfView = Mathf.Lerp(start, targetFOV, elapsed / duration);
            yield return null;
        }
        cutsceneCamera.fieldOfView = targetFOV;
    }
}