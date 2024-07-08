using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField] private Slider HPBar;
    [SerializeField] private TMP_Text HPText;

    public GameObject fire;

    float maxHP;
    public float curHP;
    public float curSpeed;
    float maxSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // HP Management
        maxHP = GameManager.Instance.CarInfo.maxHp;
        curHP = maxHP;
        SetMaxHealth(maxHP);

        maxSpeed = GameManager.Instance.CarInfo.maxSpeed;
    }

    // HP Initial Setting
    public void SetMaxHealth(float health)
    {
        HPBar.maxValue = health;
        HPBar.value = health;
    }

    // Damage control
    public void GetDamaged(float damage)
    {
        float getDamagedHP = curHP - damage;
        curHP = getDamagedHP;
        HPBar.value = curHP;
    }

    // HP Text Management
    private void UpdateHPText()
    {
        HPText.text = string.Format("{0}/{1}", (int)curHP, (int)maxHP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Car otherCar = collision.transform.GetComponent<Car>();  // ��� ��

        // Handle collision with another car
        bool thisCarIsDashing = GameManager.Instance.dash.isDash; // �� ���� ��� ����

        if (thisCarIsDashing)  // �� ���� �뽬 ���¶��
        {
            Debug.Log("dash");
            return;   // ������ ����
        }
        else
        {
            // ���� �浹�� ������
            if (otherCar != null && collision.transform.CompareTag("Car"))
            {
                Debug.Log("car accident");
                GetDamaged(collision.relativeVelocity.magnitude * 3);   // ��� ���� �ӷ¸�ŭ ������
            }
            else
            {
                Debug.Log("non car accident");
                // ���� �ƴ� ��ü�� ���ؼ� ������ �ޱ�
                GetDamaged(curSpeed * 3);
            }
        }
    }

    private void Update()
    {
        HPBar.value = curHP;
        UpdateHPText();
        curSpeed = rb.velocity.magnitude;

        // Max Speed
        if (curSpeed >= maxSpeed)
        {
            curSpeed = maxSpeed;
        }
        
        // Max Hp
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
        else if (curHP < 0)
        {
            curHP = 0;
        }

        // Game Over
        if (curHP <= 0)
        {
            Debug.Log("Game Over");
        }

        // Reset Rotation
        if (Mathf.Abs(transform.eulerAngles.z) > 80 && curSpeed < 0.03)
        {
            // Reset the z rotation to 0
            Vector3 newRotation = transform.eulerAngles;
            newRotation.z = 0;
            transform.eulerAngles = newRotation;
        }
    }
}
