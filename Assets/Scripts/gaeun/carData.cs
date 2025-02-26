using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;

public class carData : MonoBehaviour
{
    TextMeshProUGUI tmp_maxSpeed;
    TextMeshProUGUI tmp_atk;
    TextMeshProUGUI tmp_hp;
    TextMeshProUGUI tmp_dashSpeed;
    TextMeshProUGUI tmp_skill;


    carDic cardic;

    private string carName;
    private int carNum;
    private float maxSpeed;
    private float atk;
    private float hp;
    private float dashSpeed;
    private string skill;

    Slider sliderA;
    Slider sliderB;
    Slider sliderC;
    Slider sliderD;

    public static string carString;

    private void Awake()
    {
        cardic = GetComponent<carDic>();
        /*
        tmp_carName = GameObject.Find("TitanTxt").GetComponent<TextMeshProUGUI>();
        Debug.Log(tmp_carName);
        tmp_maxSpeed = GameObject.Find("TitanTxt (1)").GetComponent<TextMeshProUGUI>();
        Debug.Log(tmp_maxSpeed);
        */

    }

    private void Start()
    {
        //carObj
        //tmp_carName.text = "-";
        //tmp_maxSpeed.text = "-";

    }


    public carData(string _carName, int _carNum, float _maxSpeed, float _atk, float _hp, float _dashSpeed, string _skill)
    {
        this.carName = _carName;
        this.carNum = _carNum;
        this.maxSpeed = _maxSpeed;
        this.atk = _atk;
        this.hp = _hp;
        this.dashSpeed = _dashSpeed;
        this.skill = _skill;
    }


    public void ShowData()
    {
        carString = this.carName;

        tmp_maxSpeed = GameObject.Find("maxSpeedTmp").GetComponent<TextMeshProUGUI>();
        tmp_atk = GameObject.Find("atkTmp").GetComponent<TextMeshProUGUI>();
        tmp_hp = GameObject.Find("hpTmp").GetComponent<TextMeshProUGUI>();
        tmp_dashSpeed = GameObject.Find("dashSpeedTmp").GetComponent<TextMeshProUGUI>();
        tmp_skill = GameObject.Find("skillTmp").GetComponent<TextMeshProUGUI>();
        // why i have to reset here?????????

        Debug.Log("Clear ShowData");

        if (tmp_maxSpeed != null)
            tmp_maxSpeed.text = this.maxSpeed.ToString();
        else
            Debug.LogWarning("tmp_maxSpeed is not assigned in the Inspector.");

        if (tmp_atk != null)
            tmp_atk.text = this.atk.ToString();
        else
            Debug.LogWarning("tmp_atk is not assigned in the Inspector.");

        if (tmp_hp != null)
            tmp_hp.text = this.hp.ToString();
        else
            Debug.LogWarning("tmp_hp is not assigned in the Inspector.");

        if (tmp_dashSpeed != null)
            tmp_dashSpeed.text = this.dashSpeed.ToString();
        else
            Debug.LogWarning("tmp_dashSpeed is not assigned in the Inspector.");

        if (tmp_skill != null)
            tmp_skill.text = this.skill;
        else
            Debug.LogWarning("tmp_skill is not assigned in the Inspector.");
    }

    public void ShowCar()
    {
        //get the object with the tag "Car"
        GameObject.FindWithTag("Car").SetActive(false); // SetActive false

        Debug.Log(this.carName); // check this.car

        GameObject.Find("Car").transform.GetChild(this.carNum).gameObject.SetActive(true);
    }


    public void GageManager()
    {
        int maxSpeedMin = 50;
        int maxSpeedFull = 160;

        float atkMin = 3.0f;
        float atkFull = 7.0f;

        int hpMin = 150;
        int hpFull = 1000;

        int dashSpeedMin = 25;
        int dashSpeedFull = 120;

        // NullReferenceException - Object Reset
        sliderA = GameObject.Find("MaxSpeed Slider").GetComponent<Slider>();
        sliderB = GameObject.Find("ATK Slider").GetComponent<Slider>();
        sliderC = GameObject.Find("HP Slider").GetComponent<Slider>();
        sliderD = GameObject.Find("DASH Speed Slider").GetComponent<Slider>();

        sliderA.minValue = maxSpeedMin;
        sliderA.maxValue = maxSpeedFull;
        sliderA.value = maxSpeedFull - (this.maxSpeed - maxSpeedMin);

        sliderB.minValue = atkMin;
        sliderB.maxValue = atkFull;
        sliderB.value = atkFull - (this.atk - atkMin);

        sliderC.minValue = hpMin;
        sliderC.maxValue = hpFull;
        sliderC.value = hpFull - (this.hp - hpMin);

        sliderD.minValue = dashSpeedMin;
        sliderD.maxValue = dashSpeedFull;
        sliderD.value = dashSpeedFull - (this.dashSpeed - dashSpeedMin);

        Debug.Log(this.maxSpeed);
        Debug.Log(this.atk);
        Debug.Log(this.hp);
        Debug.Log(this.dashSpeed);
    }



}

