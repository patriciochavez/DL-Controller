using UnityEngine;
using System.Collections;
using System;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    public float turn_speed;

    Mensaje_Arduino mensaje_arduino;
    public string ws_url = "ws://192.168.1.210";
    private WebSocket w;
    public float altura;
    public float aceleracion;
    private int contador;
	private float ajuste;
    
    [Serializable]
    public class Mensaje_Arduino
    {
        public float height;
        public float acceleration;
    }

    // Use this for initialization
    IEnumerator Start()
    {
        speed = 10f;
        turn_speed = 20f;
		contador = 0;
		ajuste = 0.05f;

        w = new WebSocket(new Uri(ws_url));
        yield return StartCoroutine(w.Connect());
        if (w.error != null)
        {
            Debug.LogError("Error: " + w.error);
        }

    }

    // Update is called once per frame
    void Update()
	{
		string recibido = w.RecvString ();
		if (JsonUtility.FromJson<Mensaje_Arduino> (recibido) != null) {            
			mensaje_arduino = JsonUtility.FromJson<Mensaje_Arduino> (recibido);
			altura = mensaje_arduino.height;
            aceleracion = mensaje_arduino.acceleration;

            //transform.position = new Vector3(0, 0, -altura);
           
            if (altura > 600) {                
				if (altura > 1000) {
					transform.Translate (Vector3.down * Time.deltaTime * speed * 6);
				} else if (altura > 800) {
					transform.Translate (Vector3.down * Time.deltaTime * speed * 3);
				} else {
					transform.Translate (Vector3.down * Time.deltaTime * speed);
				}
			} else if (altura < 400) {
				if (altura < 100) {
					transform.Translate (Vector3.up * Time.deltaTime * speed * 6);
				} else if (altura < 300) {
					transform.Translate (Vector3.up * Time.deltaTime * speed * 3);
				} else { 
					transform.Translate (Vector3.up * Time.deltaTime * speed);
				}
			}


            
            
                if (aceleracion > 100)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * aceleracion/100);
                }               



        } else {
		/*	contador += 1;
		if (contador > 20) {
			transform.Translate (Vector3.down * Time.deltaTime * speed * ajuste);
			ajuste = ajuste * (-1);
			contador = 0;			
		}*/
	}
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -turn_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, turn_speed * Time.deltaTime);
        }
    }
      
}