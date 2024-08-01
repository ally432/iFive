using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreManager : MonoBehaviour
{
    // ���̾��� �ν��Ͻ�
    FirebaseFirestore db;

    // ���� ����
    public float Dash;
    public float Hp;
    public float MaxSpeed;
    public float ZeroBaek;

    public event Action OnDataReady; // �����Ͱ� �غ�Ǿ����� �˸��� �̺�Ʈ

    void Start()
    {
        // Firebase �ʱ�ȭ
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;

            // Sepia ������ ��������
            GetCarData("Sepia");
        });
    }

    void GetCarData(string carName)
    {
        DocumentReference docRef = db.Collection("carInfo").Document(carName);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log($"Document data for {carName} retrieved successfully!");

                    // �����͸� float�� ��ȯ�Ͽ� �Ҵ�
                    Dash = snapshot.GetValue<float>("Dash");
                    Hp = snapshot.GetValue<float>("Hp");
                    MaxSpeed = snapshot.GetValue<float>("MaxSpeed");
                    ZeroBaek = snapshot.GetValue<float>("ZeroBaek");

                    // ������ Ȯ��
                    Debug.Log($"Dash: {Dash}, Hp: {Hp}, MaxSpeed: {MaxSpeed}, ZeroBaek: {ZeroBaek}");

                    // �����Ͱ� �غ�Ǿ����� �˸�
                    OnDataReady?.Invoke();
                }
                else
                {
                    Debug.Log($"No such document in {carName}!");
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve document: " + task.Exception);
            }
        });
    }
}
