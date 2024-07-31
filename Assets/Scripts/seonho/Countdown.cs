using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
    public GameObject joystick;
    public TMP_Text countdownText;
    public Image background;
    private GameObject joystickInstance;

    void Start()
    {
        joystickInstance = Instantiate(joystick);
        joystickInstance.SetActive(false);
        background.gameObject.SetActive(true);
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();  // ī��Ʈ�ٿ� �ؽ�Ʈ ����
            yield return new WaitForSeconds(1f);
        }

        // ī��Ʈ�ٿ� ���� ��
        countdownText.text = "Go!";
        joystickInstance.SetActive(true);
        yield return new WaitForSeconds(1f);
        background.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);  // ī��Ʈ�ٿ� �ؽ�Ʈ ��Ȱ��ȭ
    }
}