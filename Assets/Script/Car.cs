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

        // HP ����
        maxHP = GameManager.Instance.CarInfo.maxHp;
        curHP = maxHP;
        SetMaxHealth(maxHP);

        maxSpeed = GameManager.Instance.CarInfo.maxSpeed;
    }

    // HP �ʱ� ����
    public void SetMaxHealth(float health)
    {
        HPBar.maxValue = health;
        HPBar.value = health;
    }

    // ������ ����
    public void GetDamaged(float damage)
    {
        float getDamagedHP = curHP - damage;
        curHP = getDamagedHP;
        HPBar.value = curHP;
    }

    // HP �ؽ�Ʈ ����
    private void UpdateHPText()
    {
        HPText.text = string.Format("{0}/{1}", (int)curHP, (int)maxHP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Car otherCar = collision.transform.GetComponent<Car>();  // ��� ��
        if (otherCar != null && collision.transform.CompareTag("Car"))
        {
            // Handle collision with another car
            bool thisCarIsDashing = GameManager.Instance.UIManager.isDash;
            bool otherCarIsDashing = GameManager.Instance.UIManager.isDash;

            if (thisCarIsDashing && otherCarIsDashing)
            {
                // If both cars are dashing, do nothing
                return;
            }
            else if (thisCarIsDashing)
            {
                // If this car is dashing, damage the other car
                otherCar.GetDamaged(collision.relativeVelocity.magnitude * 3);
            }
            else if (otherCarIsDashing)
            {
                GetDamaged(collision.relativeVelocity.magnitude);
            }
            else
            {
                // If neither car is dashing, apply damage normally
                GetDamaged(collision.relativeVelocity.magnitude * 3);
            }
        }
        else
        {
            // Handle collision with non-car objects
            GetDamaged(curSpeed * 3);
        }
    }

    private void Update()
    {
        HPBar.value = curHP;
        UpdateHPText();
        curSpeed = rb.velocity.magnitude;

        // �ִ� �ӷ� ����
        if (curSpeed >= maxSpeed)
        {
            curSpeed = maxSpeed;
        }
        
        // �ִ� HP ����
        if (curHP >= maxHP)
        {
            curHP = maxHP;
        }
        else if (curHP < 0)
        {
            curHP = 0;
        }

        // ���� ����
        if (curHP <= 0)
        {
            Debug.Log("Game Over");
        }

        // ������ �� �ٽ� ������
        if (Mathf.Abs(transform.eulerAngles.z) > 80 && curSpeed < 0.03)
        {
            // Reset the z rotation to 0
            Vector3 newRotation = transform.eulerAngles;
            newRotation.z = 0;
            transform.eulerAngles = newRotation;
        }
    }
}
