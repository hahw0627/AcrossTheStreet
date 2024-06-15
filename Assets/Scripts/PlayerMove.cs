using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody ActorBody = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public enum E_DirectionTye
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }
    [SerializeField]
    protected E_DirectionTye m_DirectionType = E_DirectionTye.Up;
    protected void SetActorMove(E_DirectionTye p_movetype)
    {
        Vector3 offsetpos = Vector3.zero;

        switch(p_movetype)
        {
            case E_DirectionTye.Up:
                offsetpos = Vector3.forward;
                break;
            case E_DirectionTye.Down:
                offsetpos = Vector3.back;
                break;
            case E_DirectionTye.Left:
                offsetpos = Vector3.left;
                break;
            case E_DirectionTye.Right:
                offsetpos = Vector3.right;
                break;
            default:
                Debug.LogErrorFormat("SetActorMove Error : {0}", p_movetype);
                break;
        }
        this.transform.position += offsetpos;
    }
    // Update is called once per frame
    void Update()
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
            SetActorMove (E_DirectionTye.Right);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("OnTriggerEnter : {0}, {1}", other.name, other.tag);
        if (other.tag.Contains("Crash"))
        {
            Debug.LogFormat("ºÎµóÇû´Ù");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

}
