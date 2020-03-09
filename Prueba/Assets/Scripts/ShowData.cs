using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class ShowData : MonoBehaviour
{
    public string NameJson;
    public GameObject data;
    public GameObject headers;
    public GameObject instantiateData;
    public Transform dataposition;
    public Transform ContentPosition;
    public Transform columnHeadersPosition;
    public Text[] Data;
    public Text Title;
    MembersList memberslist = new MembersList();
    public Text[] Headers;
    private void Awake()
    {
        ContentPosition = GameObject.Find("Content").GetComponent<Transform>();
        columnHeadersPosition = GameObject.Find("Column Headers").GetComponent<Transform>();
        
    }

    private void Start()
    {
        InstantiateData();
    }

    public void InstantiateData()
    {
        GameObject[] exist = GameObject.FindGameObjectsWithTag("Data");
        GameObject[] existt = GameObject.FindGameObjectsWithTag("Header");
        

        if (exist != null)
        {
            foreach (GameObject e in exist)
            {
                Destroy(e.gameObject);
            }
        }
        if (existt != null)
        {
            foreach (GameObject h in existt)
            {
                Destroy(h.gameObject);
            }
        }

        string Path = Application.streamingAssetsPath + "/" + NameJson +".json";
        string JsonString = File.ReadAllText(Path);

        MembersList memberslist = JsonUtility.FromJson<MembersList>(JsonString);

        //Load Title
        Title.text = memberslist.Title;

        //Load Column Headers
        Headers = new Text[memberslist.ColumnHeaders.Length];
        for (int h = 0; h < memberslist.ColumnHeaders.Length; h++)
        {
            //Instantiate headers
            GameObject headersInstantiated = Instantiate(headers, columnHeadersPosition.transform.position, transform.rotation);
            headersInstantiated.transform.parent = columnHeadersPosition.transform;
            headersInstantiated.transform.position = columnHeadersPosition.transform.position;
            headersInstantiated.transform.localScale = new Vector3(1, 1, 1);
            //Show headers
            Headers[h] = headersInstantiated.GetComponent<Text>();
            Headers[h].text = memberslist.ColumnHeaders[h];
        }

        //Load data
        Data = new Text[memberslist.Data.Length];
        for (int i = 0; i < memberslist.Data.Length; i++)
        {
            //Instantiate data object
            GameObject dataInstantiated = Instantiate(data, ContentPosition.transform.position, transform.rotation);
            dataInstantiated.transform.parent = ContentPosition.transform;
            dataInstantiated.transform.position = ContentPosition.transform.position;
            dataInstantiated.transform.localScale = new Vector3(1, 1, 1);
            dataposition = dataInstantiated.GetComponent<Transform>();

            //Reference to Text components
            for (int x = 0; x < memberslist.Data.Length; x++)
            {
                
                if (dataInstantiated.transform.childCount < memberslist.Data.Length)
                {
                    GameObject objinstantiated = Instantiate(instantiateData, dataposition.transform.position, transform.rotation);
                    objinstantiated.name = "data";
                    objinstantiated.transform.parent = dataposition.transform;
                    objinstantiated.transform.position = dataposition.transform.position;
                    objinstantiated.transform.localScale = new Vector3(1, 1, 1);
                    Data[x] = dataInstantiated.transform.GetChild(x).GetComponent<Text>();
                }
                else
                {
                    Data[x] = dataInstantiated.transform.GetChild(x).GetComponent<Text>();
                }
                
            }
            //Assignment array's index
            for (int e = 0; e < memberslist.Data.Length; e++)
            {
                int d = 0;
                Data[d].text = memberslist.Data[i].ID;
                d++;
                Data[d].text = memberslist.Data[i].Name;
                d++;
                Data[d].text = memberslist.Data[i].Role;
                d++;
                Data[d].text = memberslist.Data[i].Nickname;
            }
        }
    }
}


[Serializable]
public class MembersTeam
{
    public string ID;
    public string Name;
    public string Role;
    public string Nickname;
}

[Serializable]
public class MembersList
{
    public string Title;
    public string[] ColumnHeaders;
    public MembersTeam[] Data;
}