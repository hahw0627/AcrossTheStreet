using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    public Car CloneTarget = null;
    public Transform GenerationPos = null;

    public int GenerationPersent = 50;

    public float CloneDelaySec = 1.0f;

    protected float NextSecToClone = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currSec = Time.time;
        if(NextSecToClone <= currSec)
        {
            int randomval = Random.Range(0, 100);
            if(randomval <= GenerationPersent)
            {
                CloneCar();
            }
            NextSecToClone = currSec + CloneDelaySec;
        }
    }

    void CloneCar()
    {
        Transform clonepos = GenerationPos;
        Vector3 offsetpos = clonepos.position;
        offsetpos.y = 1.0f;

        GameObject cloneobj = GameObject.Instantiate(CloneTarget.gameObject, offsetpos, GenerationPos.rotation, this.transform);
        cloneobj.SetActive(true);
    }
}
