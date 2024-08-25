using UnityEngine;
using UnityEngine.Events;

public class OnTriggerBinder : MonoBehaviour
{
    public bool only_player;
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerStayEvent;
    public UnityEvent OnTriggerExitEvent;
    

    public void OnTriggerEnter(Collider touch_object)
    {
        if(only_player)
        {
            if(touch_object.gameObject.tag == "Player")
            {
                OnTriggerEnterEvent.Invoke();
            }
        }else 
        {
            OnTriggerEnterEvent.Invoke();
        }
    }
    public void OnTriggerStay(Collider touch_object)
    {
        if(only_player)
        {
            if(touch_object.gameObject.tag == "Player")
            {
                OnTriggerStayEvent.Invoke();
            }
        }else 
        {
            OnTriggerStayEvent.Invoke();
        }
    }
    public void OnTriggerExit(Collider touch_object)
    {
        if(only_player)
        {
            if(touch_object.gameObject.tag == "Player")
            {
                OnTriggerExitEvent.Invoke();
            }
        }else 
        {
            OnTriggerExitEvent.Invoke();
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
