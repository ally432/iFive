using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    private LevelManager firebaseManager;

    void Start()
    {
        firebaseManager = FindObjectOfType<LevelManager>();

        // ����: �÷��̾� ������ ������Ʈ
        string playerId = "existing_player_id"; // �̹� ������ �÷��̾� ID
        firebaseManager.UpdatePlayerData(playerId, 10, 2500);

        // ����: �÷��̾� ������ �б�
        firebaseManager.GetPlayerData(playerId);
    }
}