using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class CarSelector : MonoBehaviour
{
    [Header("Button")]
    public Button sephiaButton;
    public Button honeyBeeButton;
    public Button startButton;

    [Header("Prefab")]
    public GameObject sephiaPrefab;   // Sephia ������
    public GameObject honeyBeePrefab; // HoneyBee ������

    private CarInfo selectedCar;
    private string selectedCarName;

    private FirebaseAuth auth;         // Firebase ���� ����
    private DatabaseReference databaseReference;  // Firebase �����ͺ��̽�
    private FirebaseUser currentUser;  // ���� �α����� ����

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // ����� �α��� ���� Ȯ��
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);   // �ʱ� �������� Ȯ��

        sephiaButton.onClick.AddListener(() => SelectCar(sephiaPrefab, "Sephia"));
        honeyBeeButton.onClick.AddListener(() => SelectCar(honeyBeePrefab, "HoneyBee"));
        startButton.onClick.AddListener(SendSelectedCarDataToFirebase);
    }

    // Firebase ���� ���� ���� Ȯ��
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        // ���� ���� ����ƴ��� Ȯ��
        if (auth.CurrentUser != currentUser)
        {
            bool signedIn = currentUser != auth.CurrentUser && auth.CurrentUser != null;
            // �α׾ƿ� �ߴ��� Ȯ��
            if (!signedIn && currentUser != null)
            {
                Debug.Log("Signed out " + currentUser.UserId);
            }
            currentUser = auth.CurrentUser;  // ���� ������Ʈ
            // ���� �α����� ���� �ִ��� Ȯ��
            if (signedIn)
            {
                Debug.Log("Signed in " + currentUser.UserId);
            }
        }
    }

    // �� ����
    void SelectCar(GameObject carPrefab, string carName)
    {
        if (carPrefab != null)
        {
            selectedCar = carPrefab.GetComponent<CarInfo>();
            selectedCarName = carName;
            if (selectedCar != null)
            {
                Debug.Log($"{selectedCarName} selected with Max HP: {selectedCar.maxHp}");
            }
            else
            {
                Debug.LogError($"{carPrefab.name} prefab does not have a CarInfo component");
            }
        }
        else
        {
            Debug.LogError("Selected car prefab not found");
        }
    }

    // ���õ� �� �����͸� Firebase�� ����
    void SendSelectedCarDataToFirebase()
    {
        if (selectedCar != null && !string.IsNullOrEmpty(selectedCarName) && currentUser != null)
        {
            CarData carData = new CarData
            {
                carName = selectedCarName,
                maxHp = selectedCar.maxHp,
                maxSpeed = selectedCar.maxSpeed,
                dashSpeed = selectedCar.dashSpeed,
                zeroBaek = selectedCar.zeroBaek
            };

            string json = JsonUtility.ToJson(carData);   // CarData�� JSON �������� ��ȯ
            string userId = currentUser.UserId;          // �α����� ������� UID
            databaseReference.Child("users").Child(userId).Child("selectedCar").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data sent successfully");
                }
                else
                {
                    Debug.LogError("Error sending data: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("No car selected or user not logged in");
        }
    }
}

[System.Serializable]
public class CarData
{
    public string carName;
    public float maxHp;
    public float maxSpeed;
    public float dashSpeed;
    public float zeroBaek;
}
