using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshAstar : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public int did = 6;
    public WPnetwork wayN = null;
    private NavMeshAgent _navAgent = null;
    public List<GameObject> closedlist = new List<GameObject>();
    public Camera mainCamera = null;
    private int closedListCounter = 0;
    private int updateCounter = 0;
    // Use this for initialization	
    void Start()
    {
        Debug.Log("AgentStart");
        _navAgent = GetComponent<NavMeshAgent>();

        if (wayN == null) return;
        // SetNextDestination(false);
        GameObject source = wayN.wypoint[0];
        if (source != null)
            calhscore(source);
        GameObject des = wayN.wypoint[did - 1];

        while (des.GetComponent<WP>().fparent != null)
        {
            closedlist.Add(des);
            des = des.GetComponent<WP>().fparent;
        }
        closedlist.Add(des);

    }

    public void calfscore(GameObject source, GameObject child)
    {
        Debug.Log("Fscore calculation");
        double f = Math.Sqrt(Math.Pow(source.GetComponent<WP>().x - child.GetComponent<WP>().x, 2) + Math.Pow(source.GetComponent<WP>().y - child.GetComponent<WP>().y, 2));
        GameObject destination = wayN.wypoint[did - 1];
        if (source.GetComponent<WP>().id == 1)
        {

            child.GetComponent<WP>().fscore = f;
            child.GetComponent<WP>().gscore = calgscore(child, destination);
            child.GetComponent<WP>().hscore = child.GetComponent<WP>().fscore + child.GetComponent<WP>().gscore;
            child.GetComponent<WP>().fparent = source;

        }

        else if (child.GetComponent<WP>().fscore == 0)
        {
            child.GetComponent<WP>().fscore = f + source.GetComponent<WP>().fscore;
            child.GetComponent<WP>().gscore = calgscore(child, destination);
            child.GetComponent<WP>().hscore = child.GetComponent<WP>().fscore + child.GetComponent<WP>().gscore;
            child.GetComponent<WP>().fparent = source;

        }
        else
        {
            double fscore = f + source.GetComponent<WP>().fscore;
            if (child.GetComponent<WP>().hscore > fscore + child.GetComponent<WP>().gscore)
            {
                child.GetComponent<WP>().fscore = fscore;
                child.GetComponent<WP>().hscore = child.GetComponent<WP>().fscore + child.GetComponent<WP>().gscore;
                child.GetComponent<WP>().fparent = source;
            }

        }
    }
    //gscore calculation
    public double calgscore(GameObject child, GameObject destination)
    {
        Debug.Log("gscore calculation");
        double g = Math.Sqrt(Math.Pow(destination.GetComponent<WP>().x - child.GetComponent<WP>().x, 2) + Math.Pow(destination.GetComponent<WP>().y - child.GetComponent<WP>().y, 2));
        return g;
    }
    //hscore calculation
    public void calhscore(GameObject source)
    {
        Debug.Log("hscore calculation");
        int i = 0;
        int length = source.GetComponent<WP>().child.Count;
        while (i <= length - 1)
        {
            foreach (GameObject child in source.GetComponent<WP>().child)
            {
                calfscore(source, child);
                calhscore(child);
            }
            i++;
        }
    }
    void Update()
    {
        ++updateCounter;
        Debug.Log("in Update1");

        int i;

        int currentCounter = closedlist.Count - closedListCounter - 1;
        if (currentCounter >= 0)
        {
            //yield return new WaitForSeconds(1);
            Debug.Log("CUrrent Counter : " + currentCounter);
            Debug.Log("CUrrentx : " + closedlist[currentCounter].GetComponent<WP>().x);
            Debug.Log("CUrrenty : " + closedlist[currentCounter].GetComponent<WP>().y);
            Debug.Log("CUrrentz : " + closedlist[currentCounter].GetComponent<WP>().z);

            Vector3 newPos = new Vector3(closedlist[currentCounter].GetComponent<WP>().x, closedlist[currentCounter].GetComponent<WP>().y, closedlist[currentCounter].GetComponent<WP>().z);
            _navAgent.SetDestination(closedlist[currentCounter].transform.position);
            closedListCounter++;
        }

    }
}

    


/*if(Input.GetKeyDown(KeyCode.UpArrow))
{
    Debug.Log("in Update2");
    this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y, this.transform.position.z + 1);
}*/
