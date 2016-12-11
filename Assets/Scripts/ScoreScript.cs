using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour {
    private float currentTime = 0;
    private float quickestTime = 0;

    [SerializeField]
    private Text currentTimeText;
    [SerializeField]
    private Text quickestTimeText;
    [SerializeField]
    private Text endScreenCurrentTimeText;
    [SerializeField]
    private Text endScreenQuickestTimeText;
    [SerializeField]
    private GameObject newQuickTimeText;

    private bool finished = false;
    private void Start()
    {
        //PlayerPrefs.SetFloat("QuickestTime", Mathf.Infinity);
        quickestTime = PlayerPrefs.GetFloat("QuickestTime", Mathf.Infinity);
        if(quickestTime == Mathf.Infinity)
        {
            quickestTimeText.text = "--:--:--";
        }else
        {
            quickestTimeText.text = ConvertTimeToText(quickestTime);
        }
    }
    private void Update()
    {
        if (!finished)
        {
            currentTime += Time.deltaTime;
        }else
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        currentTimeText.text = ConvertTimeToText(currentTime);
        endScreenCurrentTimeText.text = currentTimeText.text;
    }
    private string ConvertTimeToText(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - (minutes * 60);
        string minutesString;
        string secondsString;
        if(minutes < 10)
        {
            minutesString = "0" + minutes;
        }else
        {
            minutesString = minutes.ToString();
        }
        if(seconds < 10)
        {
            secondsString = "0" + seconds;
        }else
        {
            secondsString = seconds.ToString();
        }
        return minutesString + ":" + secondsString + ":" + Mathf.Round((time - ((minutes*60) + seconds))*100);
    }
    public void Finish()
    {
        if(quickestTime == Mathf.Infinity)
        {
            endScreenQuickestTimeText.text = "--:--:--";
        }else
        {
            endScreenQuickestTimeText.text = ConvertTimeToText(quickestTime);
        }
        if(currentTime < quickestTime)
        {
            newQuickTimeText.SetActive(true);
            PlayerPrefs.SetFloat("QuickestTime", currentTime);
        }
        finished = true;
    }
}
