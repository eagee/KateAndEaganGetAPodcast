using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool Activated = false;
    public Vector3 TargetPostion = new Vector3();
    private float m_MaxDisabledTime;
    private float m_DisabledTimer;
    private float m_ActivateAfter = 10f;
    

    public Vector3 wander1;
    public Vector3 wander2;
    public float wanderSpeed;
    public float rotateSpeed;
    public float deathTimeout;

    private bool m_Dying = false;

    public void HandleDestruction()
    {
        GetComponent<Animator>().SetBool("TargetDying", true);
        m_Dying = true;
    }

    void Start()
    {
        m_Dying = false;
        m_DisabledTimer = 0f;
        m_MaxDisabledTime = .40f;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = true;
        Activated = false;
    }

    

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Activated = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Activated = false;
        }
    }
    private void HandleDeathAnimation()
    {
        bool isAnimDone = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TargetDone");
        if (m_Dying && isAnimDone)
        {
            Vector3 targetScale = new Vector3(40f, 40f, 40f);
            rotateSpeed = -240;
            transform.localScale = Vector3.Slerp(transform.localScale, targetScale, 1f * Time.deltaTime);
            deathTimeout -= 1.0f * Time.deltaTime;
            if (deathTimeout <= 0.0f)
                DestroyObject(this.gameObject, 0.5f);
        }
    }


    void Update()
    {
        m_ActivateAfter -= Time.deltaTime;
        if(m_ActivateAfter <= 0f)
        {
            Activated = true;
        }

        if(Input.GetKeyUp(KeyCode.T))
        {
            Activated = true;
        }

        HandleDeathAnimation();

        m_DisabledTimer += Time.deltaTime;
        if (m_DisabledTimer > m_MaxDisabledTime)
        {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            if (m_DisabledTimer > m_MaxDisabledTime * 2)
                GetComponent<TrailRenderer>().enabled = false;
            Vector3 w1 = this.transform.position + (Mathf.Sin(Time.time * wanderSpeed) * wander1);
            Vector3 w2 = this.transform.position + (Mathf.Cos(Time.time * wanderSpeed) * wander2);
            TargetPostion = Vector3.Lerp(w1, w2, 0.5f);
            this.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            Vector3 fixZ = this.transform.position;
            fixZ.z = 0f;
            this.transform.position = fixZ;
        }
        
        if (!Activated)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, TargetPostion, 2.5f * Time.deltaTime);
        }
    }
}


