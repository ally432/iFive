using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DatabaseManager databaseManager;
    public FirestoreManager firestoreManager;
    public Dash dash;
    public Drive drive;

    private Car car;
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

        // �� ������ ���� ����
        //databaseManager.SaveTeamRespawnArea("teamA", new Vector3(175, 0, 0));
        //databaseManager.SaveTeamRespawnArea("teamB", new Vector3(10, 3, 0));

        // �ʵ带 Find �޼��带 ����Ͽ� �ʱ�ȭ
        car = GameObject.FindFirstObjectByType<Car>();

        if (car == null)
        {
            Debug.LogError("Car object not found!");
        }

        string userId = auth.CurrentUser?.UserId;

        // ���� �� ���� �� ������ ���� �ҷ�����
        databaseManager.SaveUserData(userId, "teamA");
    }

    public void RespawnUser(string userId, Vector3 respawnArea)
    {
        if (car != null)
        {
            car.curSpeed = 0;
        }

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

    // Public �޼��带 ���� Car ��ü�� ��������
    public Car GetCar()
    {
        return car;
    }
}
