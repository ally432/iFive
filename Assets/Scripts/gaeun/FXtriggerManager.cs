using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXtriggerManager : MonoBehaviour
{
    public MapFXManager mapFXManager;
    public float areaSecBlue = 60.0f;
    public float areaSecRed = 60.0f;

    private bool isPaused = false;

    private List<string> carList = new List<string>();


    /*
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if ((filterTags?.Count ?? 0) > 0 && !filterTags.Contains(obj.tag))
        {
            return;
        }

        if (host)
        {
            host.OnTriggerEnter(other);
        }

        if (detectCounter.TryGetValue(obj, out var cnt))
        {
            detectCounter[obj] = ++cnt;
        }
        else
        {
            detectCounter[obj] = 1;
            onEnter.Invoke(obj);
        }
    }
    */





    private void OnTriggerStay(Collider collider)
    {
        string coltag = collider.gameObject.tag;
        Debug.Log("nowtag " + coltag);
        // ���
        Debug.Log(carList.Count);
        

        if (coltag == "Team Blue")
        {
            isPaused = true;
            mapFXManager.FXchangerBlue();
            CountSecBlue();            
        }

        else if (coltag == "Team Red")
        {
            isPaused = true;
            mapFXManager.FXchangerRed();
            CountSecRed();
        }

        else
        {
            if (isPaused)
            {
                if (coltag == "Team Blue") // stop yellow
                {
                    StartCoroutine(PauseCoroutineBlue(coltag));
                    Debug.Log("pause yellow");
                    isPaused = false; // restart yellowFX
                }

                else if (coltag == "Team Red") // stop yellow
                {
                    StartCoroutine(PauseCoroutineRed(coltag));
                    Debug.Log("pause yellow");
                    isPaused = false; // restart yellowFX
                }
                //else if (��̿� ��Ұ� > 2) // when car >= 2
                else if(carList.Count > 2)
                {
                    carList.RemoveAt(0);
                    carList.RemoveAt(0);
                    carList.RemoveAt(0);

                    Debug.Log("in count 0");

                    isPaused = false;
                }

            }

            else
            {
                mapFXManager.FXchangerYellow();
                Debug.Log("case Yellow");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        string coltag = other.gameObject.tag;


        if (coltag == "Team Blue")
        {
            
            // array�� �̸��߰�
            carList.Add("Team Blue");
            

        }

        else if (coltag == "Team Red")
        {
            carList.Add("Team Red");

        }

    }




    //--------------------------------------------------------

    public void CountSecBlue()
    {
        areaSecBlue -= Time.deltaTime;
    }

    public void CountSecRed()
    {
        areaSecRed -= Time.deltaTime;
    }




    private IEnumerator PauseCoroutineBlue(string coltag) // Coroutine : pause yellow
    {
        Debug.Log("blue - in coroutine");
        // ������ �ٲٰ� ����
        yield return new WaitUntil(() => coltag == "Team Blue");
    }

    private IEnumerator PauseCoroutineRed(string coltag)
    {
        Debug.Log("red - in coroutine");

        yield return new WaitUntil(() => coltag == "Team Red");
    }

    //--------------------------------------------------------

    /*switch (coltag)
    {
        case "Team Blue":
            isPaused = true;
            mapFXManager.FXchangerBlue();
            CountSecBlue();
            Debug.Log(coltag);
            if (coltag == "Untagged")
            {
                isPaused = false;
            }
            break;   


        case "Team Red":
            isPaused = true;

            mapFXManager.FXchangerRed();
            CountSecRed();
            break;


        case "Untagged": // �ǳ�?

            mapFXManager.FXchangerYellow();
            Debug.Log("case Yellow");
            break;

        default:

            if (isPaused) // pause yellow
            { /// if���ƴ϶� case�� �ؾ�? �ϴ� ����׷� ������ Ȯ��
                //Debug.Log(isPaused);
                //Debug.Log(collider.gameObject.tag);
                //Debug.Log(coltag);


                /*
                switch (collider.gameObject.tag)
                {
                    case "Team Blue":
                        Debug.Log("isPaused == true, case blue");

                        StartCoroutine(PauseCoroutineBlue(collider.gameObject.tag));
                        isPaused = false; // restart yellowFX

                        break;

                    case "Team Red":
                        Debug.Log("isPaused == true, case red");

                        StartCoroutine(PauseCoroutineRed(collider.gameObject.tag));
                        isPaused = false; // restart yellowFX

                        break;

                    default:
                        Debug.Log("Unhandled coltag value: " + collider.gameObject.tag);
                        break;
                        // blue && red �Ѵ� �����ϸ� ��������� ó���ϴ� ���̽� �߰�?
                        // �ٵ� case�� ���ǹ����� �۵��ϴ��� �𸣰ھ Ȯ���غ���
                }

            }

            else // �۵�����
            {
                mapFXManager.FXchangerYellow();
                Debug.Log("case Yellow");
            }
            break;  */


    /*
    private void OnTriggerExit(Collider other) // yellow ����
    {
        string coltag = other.gameObject.tag;
        Debug.Log(coltag);

        if (coltag == "Team Blue")
        {
            mapFXManager.FXchangerYellow();
            Debug.Log("case Yellow");
        }

        else
        {
            mapFXManager.FXchangerYellow();
            Debug.Log("case Yellow");
        }
    }*/




}
