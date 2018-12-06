using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class SimpleCharacterControl : MonoBehaviour
{

    public GameObject MessagePanel;
<<<<<<< HEAD
   
=======
    public Canvas hud;
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0

    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;

    public int papercount = 0;
    public int glasscount = 0;
    public int plasticcount = 0;

    private  int trashtag;
    public int errormessage=0;


    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }




    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
      
    }
   
    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }




    private void OnTriggerStay(Collider other)
    {
        Picking(other);
        Putting(other);


    }

   




    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    

    
    public void Picking(Collider other)
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.E) & (other.gameObject.CompareTag("paperpickup") || other.gameObject.CompareTag("plasticpickup") ||  other.gameObject.CompareTag("glasspickup") ))
=======
<<<<<<< HEAD
        
        trashtag = 0;
        if (gameObject.name.Equals("Paper")) {
            trashtag = 1;
            return;
        }
        if (gameObject.name.Equals("Glassbottle"))
        {
            trashtag = 2;
            return;
        }
        if (gameObject.name.Equals("Plasticbottle"))
        {
            trashtag = 3;
            return;
        }
        switch (trashtag)
=======
        if (Input.GetKeyDown(KeyCode.E) & (other.gameObject.CompareTag("paperpickup") || other.gameObject.CompareTag("plasticpickup") ||  other.gameObject.CompareTag("glasspickup") ))
>>>>>>> 663309a6d0d953a641049409b47e985d7ee99d9a
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
        {
            
            m_animator.SetTrigger("Pickup");
           


            if (other.gameObject.CompareTag("paperpickup"))
            {

                papercount++;
                
            }
            if (other.gameObject.CompareTag("glasspickup"))
            {

                glasscount++;
               
            }
            if (other.gameObject.CompareTag("plasticpickup"))
            {

                plasticcount++;
                
            }


            Destroy(other.gameObject);



        }





    }

    public void Putting(Collider trashcan)
    {
<<<<<<< HEAD
        if (trashcan.gameObject.CompareTag("papertrashcan") & Input.GetKeyDown(KeyCode.B))
=======
        if (trashcan.gameObject.CompareTag("papertrashcan") & Input.GetKey(KeyCode.B))
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
        {
            
            if (papercount != 0)
            {
<<<<<<< HEAD
                papercount = papercount -1;

            }
        }
        if (trashcan.gameObject.CompareTag("papertrashcan") & (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M)))
=======
                papercount--;

            }
        }
        if (trashcan.gameObject.CompareTag("papertrashcan") & (Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.M)))
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
        {
            OpenMessagePanel("");
            errormessage++;
            Second();
            
        }

<<<<<<< HEAD
        if (trashcan.gameObject.CompareTag("glasstrashcan") & Input.GetKeyDown(KeyCode.N))
=======
        if (trashcan.gameObject.CompareTag("glasstrashcan") & Input.GetKey(KeyCode.N))
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
        {            
            if (glasscount !=0)
            {
                glasscount--;

            }
       
        }
<<<<<<< HEAD
        if (trashcan.gameObject.CompareTag("glasstrashcan") & (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.M)))
        {
            OpenMessagePanel("");
            errormessage++;
            Second();
            
        }

        if (trashcan.gameObject.CompareTag("plastictrashcan") & Input.GetKeyDown(KeyCode.M))
        {
            if (plasticcount !=0)
            {
                plasticcount--;
            }

        }
        if (trashcan.gameObject.CompareTag("plastictrashcan") & (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N)))
        {
            OpenMessagePanel("");
            errormessage++;
            Second();
            
        }
    }

  

    public void OpenMessagePanel(string text) {

        MessagePanel.SetActive(true);
    }


    public void CloseMessagePanel()
    {
        
        MessagePanel.SetActive(false);
    }


    IEnumerator Second() {

        if (MessagePanel.activeSelf)
        {
            yield return new WaitForSeconds(3);
            CloseMessagePanel();
=======
        if (trashcan.gameObject.CompareTag("glasstrashcan") & (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.M)))
        {
            OpenMessagePanel("");
            errormessage++;
            Second();
            
        }

        if (trashcan.gameObject.CompareTag("plastictrashcan") & Input.GetKey(KeyCode.M))
        {
            if (plasticcount !=0)
            {
                plasticcount--;
            }

        }
        if (trashcan.gameObject.CompareTag("plastictrashcan") & (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.N)))
        {
            OpenMessagePanel("");
            errormessage++;
            Second();
            
>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
        }


        
    }

<<<<<<< HEAD
=======
  

    public void OpenMessagePanel(string text) {

        MessagePanel.SetActive(true);
    }


    public void CloseMessagePanel()
    {
        
        MessagePanel.SetActive(false);
    }


    IEnumerator Second() {

        if (MessagePanel.activeSelf)
        {
            yield return new WaitForSeconds(3);
            CloseMessagePanel();
        }


        
    }

>>>>>>> 3d3826471efdb1aa8363484a331d46b7b75cf1b0
}
