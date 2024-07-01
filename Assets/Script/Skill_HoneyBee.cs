using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HoneyBee : MonoBehaviour
{
    public void OnClick()
    {
        // �ֺ� 10m �ݰ� ���� �ִ� ���� ã��
        Collider[] carColliders = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider hitCollider in carColliders)
        {
            // ���� �±� ���� ������Ʈ ã��
            Car car = hitCollider.GetComponent<Car>();
            if (car != null)
            {
                car.curHP += 300;
            }
        }
    }
}
