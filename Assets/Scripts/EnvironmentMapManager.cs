using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMapManager : MonoBehaviour
{
    public GameObject[] EnvioronmentObjectArray;
    public Transform ParentTransform = null;
    public int MinPosZ = -20;
    public int MaxPosZ = 20;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = MinPosZ; i < MaxPosZ; ++i)
        {
            CloneRoad(i);
        }
    }
    void CloneRoad(int p_posz)
    {
        int randomindex = Random.Range(0, EnvioronmentObjectArray.Length);
        GameObject cloneobj = GameObject.Instantiate(EnvioronmentObjectArray[randomindex]);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = (float)p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if(randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
