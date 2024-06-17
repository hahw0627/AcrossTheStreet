using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnvironmentMapManager : MonoBehaviour
{
    public enum E_EnvironmentType
    {
        Grass = 0,
        Road,
        Water,

        Max
    }

    public enum E_LastRoadType
    {
        Grass = 0,
        Road,

        Max
    }

    //public GameObject[] EnvioronmentObjectArray = new GameObject[(int)E_EnvironmentType.Max];
    [Header("[CopyRoad")]
    public Road DefaultRoad = null;
    public Road WaterRoad = null;
    public GrassSpawn GrassRoad = null;

    public Transform ParentTransform = null;
    
    public int MinPosZ = -20;
    public int MaxPosZ = 20;

    public int FrontOffsetPosZ = 10;
    public int BackOffsetPosZ = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    public int GroupRandomRoadLine(int p_posz)
    {
        int randomcount = Random.Range(1, 4);

        for(int i = 0; i < randomcount; ++i)
        {
            GeneratorRoadLine(p_posz + i);
        }
        return randomcount;
    }

    public int GroupRandomWaterLine(int p_posz)
    {
        int randomcount = Random.Range(1, 4);

        for (int i = 0; i < randomcount; ++i)
        {
            GeneratorWarterLine(p_posz + i);
        }
        return randomcount;
    }

    public int GroupRandomGrassLine(int p_posz)
    {
        int randomcount = Random.Range(1, 3);

        for (int i = 0; i < randomcount; ++i)
        {
            GeneratorGrassLine(p_posz + i);
        }
        return randomcount;
    }

    public void GeneratorRoadLine(int p_posz)
    {
        GameObject cloneobj = GameObject.Instantiate(DefaultRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = (float)p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if (randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        cloneobj.name = "GrassRoad_" + p_posz.ToString();

        m_LineMapList.Add(cloneobj.transform);
        m_LineMapDic.Add(p_posz, cloneobj.transform);
    }

    public void GeneratorWarterLine(int p_posz)
    {
        GameObject cloneobj = GameObject.Instantiate(WaterRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = (float)p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        int randomrot = Random.Range(0, 2);
        if (randomrot == 1)
        {
            cloneobj.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        cloneobj.name = "GrassWater_" + p_posz.ToString();

        m_LineMapList.Add(cloneobj.transform);
        m_LineMapDic.Add(p_posz, cloneobj.transform);
    }

    public void GeneratorGrassLine(int p_posz)
    {
        GameObject cloneobj = GameObject.Instantiate(GrassRoad.gameObject);
        cloneobj.SetActive(true);
        Vector3 offsetpos = Vector3.zero;
        offsetpos.z = (float)p_posz;
        cloneobj.transform.SetParent(ParentTransform);
        cloneobj.transform.position = offsetpos;

        cloneobj.name = "GrassLine_" + p_posz.ToString();

        m_LineMapList.Add(cloneobj.transform);
        m_LineMapDic.Add(p_posz, cloneobj.transform);
    }


    protected E_LastRoadType m_LastRoadType = E_LastRoadType.Max;
    protected List<Transform> m_LineMapList = new List<Transform>();
    protected Dictionary<int, Transform> m_LineMapDic = new Dictionary<int,Transform>();
    protected int m_LastLinePos = 0;

    protected int m_MinLine = 0;
    public int m_DeleteLine = 10;
    public int m_BackOffetLineCount = 20;
    public void UpdateFowardNBackMove(int p_posz)
    {
        if (m_LineMapList.Count <= 0)
        {
            m_LastRoadType = E_LastRoadType.Grass;
            m_MinLine = MinPosZ;
            int i = 0;
            // 초기용 라인들 세팅
            for (i = MinPosZ; i < MaxPosZ; ++i)
            {
                int offsetval = 0;
                if (i < 0)
                {
                    GeneratorGrassLine(i);
                }
                else
                {
                    if (m_LastRoadType == E_LastRoadType.Grass)
                    {
                        int ranomval = Random.Range(0, 2);
                        if (ranomval == 0)
                        {
                            offsetval = GroupRandomWaterLine(i);
                        }
                        else
                        {
                            offsetval = GroupRandomRoadLine(i);

                        }

                        m_LastRoadType = E_LastRoadType.Road;

                    }
                    else
                    {
                        offsetval = GroupRandomGrassLine(i);
                        m_LastRoadType = E_LastRoadType.Grass;

                    }
                    i += offsetval - 1;
                }
            }
            m_LastLinePos = i;
        }

        // 새롭게 생성
        if (m_LastLinePos < p_posz + FrontOffsetPosZ)
        {
            int offsetval = 0;
            if (m_LastRoadType == E_LastRoadType.Grass)
            {
                int ranomval = Random.Range(0, 2);
                if (ranomval == 0)
                {
                    offsetval = GroupRandomWaterLine(m_LastLinePos);
                }
                else
                {
                    offsetval = GroupRandomRoadLine(m_LastLinePos);

                }

                m_LastRoadType = E_LastRoadType.Road;

            }
            else
            {
                offsetval = GroupRandomGrassLine(m_LastLinePos);
                m_LastRoadType = E_LastRoadType.Grass;

            }
            m_LastLinePos += offsetval;
        }

        // 많이 지나갔으면 지우기
        if (p_posz -m_BackOffetLineCount > m_MinLine - m_DeleteLine)
        {
            int count = m_MinLine + m_DeleteLine;
            for(int i = m_MinLine; i<count; ++i)
            {
                RemoveLine(i);
            }

            m_MinLine += m_DeleteLine;
        }
    }
    void RemoveLine(int p_posz)
    {
        if (m_LineMapDic.ContainsKey(p_posz))
        {
            Transform transobj = m_LineMapDic[p_posz];
            GameObject.Destroy(transobj.gameObject);

            m_LineMapList.Remove(transobj);
            m_LineMapDic.Remove(p_posz);
        }
        else
        {
            Debug.LogErrorFormat("RemoveLine Error : {0}", p_posz);
        }
    }
}
