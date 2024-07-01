using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HoneyBee : MonoBehaviour
{
    public void OnClick()
    {
        // HoneyBee �ֺ� 10m �ݰ� ���� �ִ� �������� ã���ϴ�.
        Collider[] carColliders = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider hitCollider in carColliders)
        {
            // ���� �±׸� ���� ������Ʈ�� ã���ϴ�.
            Car car = hitCollider.GetComponent<Car>();
            if (car != null)
            {
                car.curHP += 300;
            }
        }
    }
}
