using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DatabaseManager databaseManager;
    public CarInfo CarInfo;
    public Car car;
    public Dash dash;
    public Drive drive;

    private FirebaseAuth auth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // �����ͺ��̽� �ʱ�ȭ
        //databaseManager.ResetDatabase();

        // �� ������ ���� ����
        databaseManager.SaveTeamRespawnArea("teamA", new Vector3(0, 3, 0));
        databaseManager.SaveTeamRespawnArea("teamB", new Vector3(10, 3, 0));

        string userId = auth.CurrentUser?.UserId;

        // ���� �� ���� �� ������ ���� �ҷ�����
        databaseManager.SaveUserData(userId, "teamA");
    }

    public void RespawnUser(string userId, Vector3 respawnArea)
    {
        GameManager.Instance.car.curSpeed = 0;
        // ���� ������ ���� (��: ������ ��ġ�� ������ �������� �̵�)
        Debug.Log($"Respawning user {userId} at {respawnArea}");

        // ���� ���� ������Ʈ �̵� ���� ����
        GameObject userObject = FindLocalPlayerObject();
        if (userObject != null)
        {
            userObject.transform.position = respawnArea;
        }
        else
        {
            Debug.LogError("User object not found!");
        }
    }

    GameObject FindLocalPlayerObject()
    {
        GameObject localPlayerObject = GameObject.FindGameObjectWithTag("Car");
        if (localPlayerObject != null)
        {
            Debug.Log("Local player object found: " + localPlayerObject);
            return localPlayerObject;
        }
        else
        {
            Debug.LogError("Local player object not found!");
            return null;
        }
    }
}
