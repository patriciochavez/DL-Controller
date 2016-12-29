using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay_Controller : MonoBehaviour {
	public bool displayEnabled;
	public GameObject Player;

	public Text height_Display_L;
	public Text height_Display_R;
	private string height_Player;
    public Text accel_Display_L;
    public Text accel_Display_R;
    private string accel_Player;
    private int contador;
    private Vector3 speed;

    Vector3 lastPos = Vector3.zero;

    // Use this for initialization
    void Start () {
		displayEnabled = true;
        accel_Player = "0";
	}
	
	// Update is called once per frame
	void Update () {
		if (displayEnabled) {

            contador += 1;

			height_Player = Mathf.Round(Player.transform.position.y).ToString();
			height_Display_L.text = "h: " + height_Player;
			height_Display_R.text = "h:" + height_Player;

            if (contador > 10) { 
            Vector3 distancePerFrame = transform.position - lastPos;
            lastPos = transform.position;
            speed = distancePerFrame * 3;
                contador = 0;
            }

            accel_Player = Mathf.Round(Mathf.Sqrt(Mathf.Pow(speed.x, 2f) + Mathf.Pow(speed.y, 2f) + Mathf.Pow(speed.z, 2f))).ToString();
            accel_Display_L.text = "a: " + accel_Player;
            accel_Display_R.text = "a:" + accel_Player;
        }        
    }   
}
