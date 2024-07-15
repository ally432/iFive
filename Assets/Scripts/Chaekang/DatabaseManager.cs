using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // �����ͺ��̽� �ʱ�ȭ
    public void ResetDatabase()
    {
        // ��� ������ ����
        databaseReference.RemoveValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("All data removed successfully.");
                // �ʱ� ������ ����
            }
            else
            {
                Debug.LogError("Failed to remove data: " + task.Exception);
            }
        });
    }

    // �� ������ ���� ����
    public void SaveTeamRespawnArea(string team, Vector3 respawnArea)
    {
        TeamRespawnArea teamRespawnArea = new TeamRespawnArea(respawnArea);
        string json = JsonUtility.ToJson(teamRespawnArea);
        databaseReference.Child("teams").Child(team).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Team respawn area saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save team respawn area: " + task.Exception);
            }
        });
    }

    // �� ������ ���� �ҷ�����
    public void GetTeamRespawnArea(string team, System.Action<Vector3> onRespawnAreaLoaded)
    {
        databaseReference.Child("teams").Child(team).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                TeamRespawnArea teamRespawnArea = JsonUtility.FromJson<TeamRespawnArea>(snapshot.GetRawJsonValue());
                onRespawnAreaLoaded?.Invoke(teamRespawnArea.respawnArea);
            }
            else
            {
                Debug.LogError("Failed to load team respawn area: " + task.Exception);
                onRespawnAreaLoaded?.Invoke(Vector3.zero); // �⺻�� ��ȯ
            }
        });
    }

    // ���� ������ ����
    public void SaveUserData(string userId, string team)
    {
        UserData userData = new UserData(team);
        string json = JsonUtility.ToJson(userData);
        databaseReference.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("User data saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save user data: " + task.Exception);
            }
        });
    }

    // ���� ������ �ҷ�����
    public void LoadUserData(string userId, System.Action<UserData> onUserDataLoaded)
    {
        databaseReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                UserData userData = JsonUtility.FromJson<UserData>(snapshot.GetRawJsonValue());
                onUserDataLoaded?.Invoke(userData);
            }
            else
            {
                Debug.LogError("Failed to load user data: " + task.Exception);
            }
        });
    }

    // ���� �� ���� �ҷ�����
    public void GetUserTeam(string userId, System.Action<string> onTeamLoaded)
    {
        LoadUserData(userId, userData =>
        {
            if (userData != null)
            {
                onTeamLoaded?.Invoke(userData.team);
            }
            else
            {
                Debug.LogError("Failed to load user data.");
                onTeamLoaded?.Invoke(null); // �⺻�� ��ȯ
            }
        });
    }
}

[System.Serializable]
public class TeamRespawnArea
{
    public Vector3 respawnArea;

    public TeamRespawnArea(Vector3 respawnArea)
    {
        this.respawnArea = respawnArea;
    }
}

[System.Serializable]
public class UserData
{
    public string team;

    public UserData(string team)
    {
        this.team = team;
    }
}
