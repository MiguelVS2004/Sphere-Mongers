using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private GameObject Timer;
    private Text timer;
    private float currentTime;
    private int currentdiplay;
    private GameObject finish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = GameObject.Find("Timer");
        timer = Timer.GetComponent<Text>();
        finish = GameObject.Find("Finish");
        finish.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        currentdiplay = 30 - (int)currentTime;

        timer.text = "" + currentdiplay;

        if (currentdiplay <= 0)
        {
            finish.SetActive(true);
        }
    }
}
