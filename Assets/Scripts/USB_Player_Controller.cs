using UnityEngine;
using System.Collections;
using System;

public class USB_Player_Controller : MonoBehaviour
{
    public float speed;
    public float turn_speed;

    USB_Game_Controller usb_game_controller;
    public string ws_url = "wss://192.168.1.39:8443";
    private WebSocket w;
    public float altura;
    public float aceleracion;
    private int contador;
    private float ajuste;

    private string order;

    [Serializable]
    public class USB_Game_Controller
    {
        public string motion;        
    }

    // Use this for initialization
    IEnumerator Start()
    {
        speed = 3f;
        turn_speed = 10f;
        contador = 0;
        ajuste = 0.05f;
        order = "stop";

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
        string recibido = w.RecvString();
        if (JsonUtility.FromJson<USB_Game_Controller>(recibido) != null)
        {
            usb_game_controller = JsonUtility.FromJson<USB_Game_Controller>(recibido);
            order = usb_game_controller.motion;
        }
        if (order != "stop")
            {
                //if (Input.GetKey(KeyCode.UpArrow))
                if (order == "move_up")
                {
                    transform.Translate(Vector3.up * Time.deltaTime * speed);
                }
                //if (Input.GetKey(KeyCode.DownArrow))
                if (order == "move_down")
                {
                    transform.Translate(Vector3.down * Time.deltaTime * speed);
                }
                //if (Input.GetButton("Fire1"))
                if (order == "move_forward")
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                //if (Input.GetKey(KeyCode.LeftArrow))
                if (order == "move_left")
                {
                    transform.Rotate(Vector3.up, -turn_speed * Time.deltaTime);
                }
                //if (Input.GetKey(KeyCode.RightArrow))
                if (order == "move_right")
                {
                    transform.Rotate(Vector3.up, turn_speed * Time.deltaTime);
                }
            }        
    }
}
