using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameLoader : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    [SerializeField]
    private Image loadingBar;
    // Use this for initialization
    void Start()
    {
        //
       
        LoadScene();
        StartCoroutine(Interface());
        Application.backgroundLoadingPriority = ThreadPriority.High;
        //DontDestroyOnLoad(audio.gameObject);

        //Debug.Log("NOW: "+DailyGiftPanel.GetNistTime());
    }
    //string current = "1";
    //private void OnGUI()
    //{
    //    current=GUILayout.TextField(current, GUILayout.Width(300), GUILayout.Height(200));
    //    if (GUILayout.Button("LOAD", GUILayout.Width(200), GUILayout.Height(200)))
    //    {
    //        Application.LoadLevel(int.Parse(current));
    //    }
    //}
    //void LoadScene(bool isOnlineMode)
    //{
    //    LoadScene();
    //}
    public void LoadScene()
    {
        StartCoroutine(Load());
    }
    IEnumerator Interface()
    {
        while (enabled)
        {
            text.text = "Loading";
            yield return new WaitForSeconds(0.15f);
            text.text = "Loading.";
            yield return new WaitForSeconds(0.15f);
            text.text = "Loading..";
            yield return new WaitForSeconds(0.15f);
            text.text = "Loading...";
            yield return new WaitForSeconds(0.15f);
            text.text = "Loading..";
            yield return new WaitForSeconds(0.15f);
            text.text = "Loading.";
            yield return new WaitForSeconds(0.15f);
        }
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.1f);
        //string lang = PlayerPrefs.GetString("Language", "en");
        //switch (lang)
        //{
        //    case "vi":
        //        {
        //            Resources.LoadAsync("Assets/Resources/BeVietnam-Regular SDF");
        //            break;
        //        }
        //    case "jp":
        //        {
        //            Resources.LoadAsync("Assets/Resources/Kosugi-Regular SDF");
        //            break;
        //        }
        //    case "kr":
        //        {
        //            Resources.LoadAsync("Assets/Resources/NotoSansKR-Medium SDF");
        //            break;
        //        }
        //    case "ru":
        //        {
        //            Resources.LoadAsync("Assets/Resources/BOOKOSB SDF 1");
        //            break;
        //        }
        //    case "in":
        //        {
        //            Resources.LoadAsync("Assets/Resources/NotoSans-Regular SDF");
        //            break;
        //        }
        //}
        AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        float timer = 0;
        loadingBar.fillAmount = 0;
        while (async.progress < 0.9f)
        {
            loadingBar.fillAmount = async.progress;

            timer += Time.deltaTime;
            yield return null;
        }


        float t = 2f - timer;
        timer = 0;
        while (timer < t)
        {
            loadingBar.fillAmount = 0.45f + 0.55f * timer / t;
            timer += Time.deltaTime;
            yield return null;
        }
        loadingBar.fillAmount = 1;
        yield return new WaitForSeconds(0.3f);
        async.allowSceneActivation = true;

    }
}
