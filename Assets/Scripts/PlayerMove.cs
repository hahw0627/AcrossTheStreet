using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody ActorBody = null;
    public EnvironmentMapManager EnvironmentMapManagerCom = null;
    // Start is called before the first frame update
    void Start()
    {
        string[] templayer = new string[] { "Plant" };
        m_TreeLayerMask = LayerMask.GetMask(templayer);

        EnvironmentMapManagerCom.UpdateFowardNBackMove((int)this.transform.position.z);
    }
    public enum E_DirectionType
    {
        Up = 0,
        Down,
        Left,
        Right
    }
    [SerializeField]
    protected E_DirectionType m_DirectionType = E_DirectionType.Up;
    protected int m_TreeLayerMask = -1;

    protected bool ISCheckDierectionViewMove(E_DirectionType p_movetype)
    {
        Vector3 direction = Vector3.zero;

        switch (p_movetype)
        {
            case E_DirectionType.Up:
                {
                    direction = Vector3.forward;
                }
                break;
            case E_DirectionType.Down:
                {
                    direction = Vector3.back;
                }
                break;
            case E_DirectionType.Left:
                {
                    direction = Vector3.left;
                }
                break;
            case E_DirectionType.Right:
                {
                    direction = Vector3.right;
                }
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_movetype);
                break;
        }
        RaycastHit hitobj;
        if (Physics.Raycast(this.transform.position, direction, out hitobj, 1.0f, m_TreeLayerMask))
        {
            return false;
        }
        return true;
    }
    public Transform ChildModel = null;  
    void SetDirectionRot(E_DirectionType p_movetype)
    {
        switch(p_movetype)
        {
            case E_DirectionType.Up:
                ChildModel.rotation = Quaternion.identity;
                break;
            case E_DirectionType.Down:
                ChildModel.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case E_DirectionType.Left:
                ChildModel.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case E_DirectionType.Right:
                ChildModel.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
    }

    protected void SetActorMove(E_DirectionType p_movetype)
    {
        if (!ISCheckDierectionViewMove(p_movetype))
        {
            return;
        }
        Vector3 offsetpos = Vector3.zero;

        switch(p_movetype)
        {
            case E_DirectionType.Up:
                {
                    offsetpos = Vector3.forward;
                }
                break;
            case E_DirectionType.Down:
                {
                    offsetpos = Vector3.back;
                }
                break;
            case E_DirectionType.Left:
                {
                    offsetpos = Vector3.left;
                }
                break;
            case E_DirectionType.Right:
                {
                    offsetpos = Vector3.right;
                }
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_movetype);
                break;
        }

        SetDirectionRot(p_movetype);

        this.transform.position += offsetpos;
        m_Raft0ffsetpos += offsetpos;

        EnvironmentMapManagerCom.UpdateFowardNBackMove((int)this.transform.position.z);
    }
    protected void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(E_DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(E_DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(E_DirectionType.Right);
        }
    }
    Vector3 m_Raft0ffsetpos = Vector3.zero;
    protected void UpdateRaft()
    {
        if(RaftObject == null)
        {
            return;
        }
        Vector3 actorpos = RaftObject.transform.position + m_Raft0ffsetpos; 
        this.transform.position = actorpos;
    }
    void Update()
    {
        InputUpdate();
        UpdateRaft();


    }
    [SerializeField]
    protected Raft RaftObject = null;
    protected Transform RaftCompareObj = null;
    public void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("OnTriggerEnter : {0}, {1}", other.name, other.tag);
        if (other.tag.Contains("Raft"))
        {
            RaftObject = other.transform.parent.GetComponent<Raft>();
            if(RaftObject != null)
            {
                RaftCompareObj = RaftObject.transform;
                m_Raft0ffsetpos = this.transform.position - RaftObject.transform.position;
            }
            Debug.LogFormat("¶Â¸ñÅÀ´Ù : {0}, {1}", other.name, m_Raft0ffsetpos);
            return;
        }
        if (other.tag.Contains("Crash"))
        {
            Debug.LogFormat("ºÎµóÇû´Ù");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.LogFormat("OnTriggerExit : {0}, {1}", other.name, other.tag);
        if (other.tag.Contains("Raft") && RaftCompareObj == other.transform.parent)
        {
            RaftCompareObj = null;
            RaftObject = null;
            m_Raft0ffsetpos = Vector3.zero;
        }
         
    }

}
