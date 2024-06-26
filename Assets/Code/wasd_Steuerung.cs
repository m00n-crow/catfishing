using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;


//steuert das bewegen des Spielers (haken) mit WASD
public class wasd_Steuerung : MonoBehaviour
{

    
    public Throwable throwable;


    
    
    // Start is called before the first frame update

    [SerializeField] 
    private float _speed = 5f;


    void Awake()
    {
       // transform.position = new Vector3(0, 0, 0); 
       
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateMovement();

        if (throwable != null)
        {
            //Debug.Log("Multiplier value: " + throwable.wasdSteuerung);

            // HIER ENTSTEHT DER ERROR (drüber)
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        
        //transform position weggelassen, da es limitiert und camera movment avoiden würde(?) : nicht gewünscht
        
       
    }
}
