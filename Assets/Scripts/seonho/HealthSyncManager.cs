using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSyncManager : MonoBehaviourPun
{
    private Car carScript;
    private HPBarManager hpBarManager;

    private void Start()
    {
        carScript = GetComponent<Car>();
        hpBarManager = GetComponent<HPBarManager>();

        if (carScript != null && hpBarManager != null)
        {
            // �ʱ� ü�� ������ �ٸ� Ŭ���̾�Ʈ���� ����
            photonView.RPC("UpdateHealthFromRPC", RpcTarget.All, carScript.curHP);
        }
    }

    private void Update()
    {
        // ü�� ������ ����Ǿ����� Ȯ��
        if (carScript != null && hpBarManager != null)
        {
            // ü�� ������ ��Ʈ��ũ�� ����
            photonView.RPC("UpdateHealthFromRPC", RpcTarget.All, carScript.curHP);
        }
    }

    [PunRPC]
    public void UpdateHealthFromRPC(float newHealth)
    {
        if (hpBarManager != null)
        {
            hpBarManager.UpdateHealth(newHealth);
        }
    }
}