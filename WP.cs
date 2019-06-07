using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WP : MonoBehaviour {

    // Use this for initialization
    public int id;
    public float x;
    public float y;
    public float z;
    public double fscore;
    public double gscore ;
    public double hscore;
    public List<GameObject> parent = new List<GameObject>();
    public List<GameObject> child = new List<GameObject>();
    public GameObject fparent;

}
