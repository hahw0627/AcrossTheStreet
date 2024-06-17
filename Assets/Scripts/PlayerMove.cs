using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody ActorBody = null;

    // Start is called before the first frame update
    void Start()
    {
        string[] templayer = new string[] { "Plant" };
        m_TreeLayerMask = LayerMask.GetMask(templayer);
    }
    public enum E_DirectionTye
    {
        Up = 0,
        Down,
        Left,
        Right
    }
    [SerializeField]
    protected E_DirectionTye m_DirectionType = E_DirectionTye.Up;
    protected int m_TreeLayerMask = -1;

    protected bool ISCheckDierectionViewMove(E_DirectionTye p_movetype)
    {
        Vector3 direction = Vector3.zero;

        switch (p_movetype)
        {
            case E_DirectionTye.Up:
                {
                    direction = Vector3.forward;
                }
                break;
            case E_DirectionTye.Down:
                {
                    direction = Vector3.back;
                }
                break;
            case E_DirectionTye.Left:
                {
                    direction = Vector3.left;
                }
                break;
            case E_DirectionTye.Right:
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
    protected void SetActorMove(E_DirectionTye p_movetype)
    {
        if (!ISCheckDierectionViewMove(p_movetype))
        {
            return;
        }
        Vector3 offsetpos = Vector3.zero;

        switch(p_movetype)
        {
            case E_DirectionTye.Up:
                {
                    offsetpos = Vector3.forward;
                }
                break;
            case E_DirectionTye.Down:
                {
                    offsetpos = Vector3.back;
                }
                break;
            case E_DirectionTye.Left:
                {
                    offsetpos = Vector3.left;
                }
                break;
            case E_DirectionTye.Right:
                {
                    offsetpos = Vector3.right;
                }
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_movetype);
                break;
        }
        this.transform.position += offsetpos;

        m_Raft0ffsetpos += offsetpos;
    }
    protected void InputUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetActorMove(E_DirectionTye.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetActorMove(E_DirectionTye.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetActorMove(E_DirectionTye.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetActorMove(E_DirectionTye.Right);
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
