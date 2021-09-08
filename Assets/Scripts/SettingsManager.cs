using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider sliderBall;
    public TextMeshProUGUI txtBall;
    public Slider sliderSpeed;
    public TextMeshProUGUI txtSpeed;


    // Start is called before the first frame update
    void Start()
    {
        int initBall = SceneDataPersistence.Instance.BallNumber;
        float initSpeed = SceneDataPersistence.Instance.Speed;
        sliderBall.value = initBall;
        sliderSpeed.value = initSpeed;
        txtBall.text = initBall + "";
        txtSpeed.text = System.Math.Round(initSpeed, 2) + "";
        SceneDataPersistence.Instance.SetBallAndSpeed((int)initBall, (float)System.Math.Round(initSpeed, 2));
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnSliderChange()
    {
        txtBall.text = sliderBall.value + "";
        txtSpeed.text = System.Math.Round(sliderSpeed.value, 2) + "";

        SceneDataPersistence.Instance.SetBallAndSpeed((int)sliderBall.value, (float)System.Math.Round(sliderSpeed.value, 2));
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
