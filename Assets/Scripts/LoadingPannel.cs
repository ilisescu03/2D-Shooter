using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingPannel : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingPannelObject;
    [SerializeField]
    private Image LoadingFillImage;
    [SerializeField]
    private Text LoadingText;
    [SerializeField]
    private float loadingDuration = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        LoadingText.text = "Loading...";
        LoadingFillImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       // StartCoroutine(LoadingTextUpdate());
    }
    public IEnumerator LoadingTextUpdate()
    {
        yield return new WaitForSeconds(3f);
        if (LoadingText.text == "Loading...")
        {
            LoadingText.text = "Loading.";
        }
        else if (LoadingText.text == "Loading..")
        {
            LoadingText.text = "Loading...";
        }
        else if (LoadingText.text == "Loading.")
        {
            LoadingText.text = "Loading..";
        }
    }
    public void Hide()
    {
        StartCoroutine(Loading());
    }
    public void Show()
    {
        LoadingPannelObject.SetActive(true);
    }
    public IEnumerator Loading()
    {
        float elapsed = 0f;
        LoadingFillImage.fillAmount = 0f;
        while (elapsed < loadingDuration)
        {
            elapsed += Time.deltaTime;
            LoadingFillImage.fillAmount = Mathf.Clamp01(elapsed / loadingDuration);
            yield return null;
        }

        LoadingFillImage.fillAmount = 1f;
        LoadingPannelObject.SetActive(false);
    }
    public bool isLoading()
    {
        return LoadingPannelObject.activeSelf;
    }
}
