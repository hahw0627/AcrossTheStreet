using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn : MonoBehaviour
{
    public List<Transform> EnvirmentObjectList = new List<Transform>();
    public int StartMinVal = -12;
    public int StartMaxVal = 12;

    public int SpawnCreateRandom = 50;

    void GeneratorEnvirment()
    {
        int randomindex = 0;
        int randomval = 0;
        GameObject tempclone = null;
        Vector3 offsetpos = Vector3.zero;
        for (int i = StartMinVal; i < StartMaxVal; ++i)
        {
            randomval = Random.Range(0, 100);
            if(randomval < SpawnCreateRandom)
            {
                randomindex = Random.Range(0, EnvirmentObjectList.Count);
                tempclone = GameObject.Instantiate(EnvirmentObjectList[randomindex].gameObject);
                tempclone.SetActive(true);
                offsetpos.Set(i, 1f, 0f);   
                tempclone.transform.SetParent(this.transform);
                tempclone.transform.localPosition = offsetpos;
            }
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        GeneratorEnvirment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
