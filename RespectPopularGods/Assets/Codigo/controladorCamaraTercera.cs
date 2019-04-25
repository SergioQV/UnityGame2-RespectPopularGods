using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorCamaraTercera : MonoBehaviour {

    public Transform lookAt;
    public Transform cameraTransform;
    
    private const float angulo_minimo_y = 8.0f;
    private const float angulo_maximo_y = 80.0f;

    private float distancia = 2.0f;
    private float actualX = 0.0f;
    private float actualY = 13.8f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    

    private void Start()
    {
        cameraTransform = transform;
    }

    private void Update()
    {
        actualX += Input.GetAxis("Mouse X")*sensivityX;
        actualY += Input.GetAxis("Mouse Y")*sensivityY;
        
        actualY = Mathf.Clamp(actualY, angulo_minimo_y, angulo_maximo_y);

    }

    private void LateUpdate()
    {
            Vector3 dir = new Vector3(0, 0, -distancia);
            Quaternion rotacion = Quaternion.Euler(actualY, actualX, 0);
            cameraTransform.position = lookAt.position + rotacion * dir;
            cameraTransform.LookAt(lookAt.position);
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + 1, cameraTransform.position.z);
    }

    


}


