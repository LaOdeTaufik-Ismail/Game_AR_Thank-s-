using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_LaunchForce = 5f; 

          
    private float m_ChargeSpeed;         
    public bool m_Fired;          
    JoyButton joybutton;


    private void OnEnable()
    {
        
    }


    private void Start()
    {
        joybutton = FindObjectOfType<JoyButton>();
    }


    private void Update()
    {
       if (joybutton.Pressed && !m_Fired)
        {
         
            Fire();
        }
        else if (!joybutton.Pressed)
        {
        
            m_Fired = false;
     
        }

    }


    private void Fire()
    {
       
        m_Fired = true;

        
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

       
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

        Debug.Log("_______________________Awal Posisi Peluru_____________________________");
        Debug.Log(m_FireTransform.forward.ToString("F5"));
        Debug.Log("_______________________Tujuan Peluru_____________________________");
        Debug.Log(shellInstance.velocity.ToString("F5"));


        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

}