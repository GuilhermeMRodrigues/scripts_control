using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;
using System;


public class Alou : MonoBehaviour
{
    static UdpClient udpClient;
    [SerializeField] private bool vertical = true;
    [SerializeField] private bool horizontal = true;

    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    [SerializeField] private float inicial_x = 9999;
    [SerializeField] private int final_x = 9999;
    [SerializeField] private float inicial_y = 9999;
    [SerializeField] private int final_y = 9999;
    [SerializeField] private int portNum = 9999;

    [SerializeField] private float speed = 3f; // move speed
    private bool gameStart = false; 
    private void Start()
    {
        
        udpClient = new UdpClient(portNum);
        print("Start");
    }

    void Update()
    {
        float monitorX = transform.position.x;
        float monitorY= transform.position.y;

        try{
            IPEndPoint remoteEP = null;
		    byte[] data = udpClient.Receive(ref remoteEP);
		    string message = Encoding.ASCII.GetString(data);  
            //float posx = float.Parse(message); 
            //float posy = float.Parse(message);
        

            string aux = message.Replace(" ", "");

            string[] coordenates = aux.Split(','); 
            

            if(coordenates.Length == 2){
                float x = float.Parse(coordenates[0]);
                float y = float.Parse(coordenates[1]);
                
                print(gameStart);
                float x_posi =  x*-1;
                float y_posi =  y*-1;
                print($"{x}, {y}");

                if((x_posi > inicial_x && x < final_x)  && (y_posi > inicial_y && y < final_y)){
                    gameStart = true;
                    print(gameStart);
                }

                if(gameStart){

                    monitorX = x*speed;
                    monitorY = y*speed;
                    
                    print($"{monitorX}, {monitorY}");
 

                if(vertical && horizontal){
                        Debug.LogWarning("Alou");
                        //transform.position = new Vector3(monitorX, monitorY, transform.position.z);
                        transform.position = Vector2.MoveTowards(new Vector2(monitorX, monitorY), transform.position, speed);
                    }else if(vertical){
                        transform.position = new Vector3(transform.position.x, monitorY, transform.position.z);
                    }else if(horizontal){
                        transform.position = new Vector3(monitorX, transform.position.y, transform.position.z);
                    }
                }
            }

            
        
        }catch(Exception e){
            print(e);
        }
        
        if(transform.position.x >= limitX){
            this.gameObject.transform.position = new Vector3(limitX, transform.position.y, transform.position.z);
        }
        if(transform.position.x <= -limitX){
            this.gameObject.transform.position = new Vector3(-limitX, transform.position.y, transform.position.z);
        }
         if(transform.position.y >= limitY){
            this.gameObject.transform.position = new Vector3(transform.position.x, limitY, transform.position.z);
        }
        if(transform.position.y <= -limitY){
            this.gameObject.transform.position = new Vector3(transform.position.x, -limitY, transform.position.z);
        }

        //float posx = float.Parse(message);

        /* -------- Vertical Moviment -------- */
        if (Input.GetKey(KeyCode.UpArrow)) {
            print("up");
            monitorY += speed;
            this.gameObject.transform.position = new Vector3(transform.position.x, monitorY, transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            print("down");
            monitorY -= speed;
            this.gameObject.transform.position = new Vector3(transform.position.x, monitorY, transform.position.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            print("left");
            monitorX -= speed;
            this.gameObject.transform.position = new Vector3(monitorX, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            print("right");
            monitorX += speed;
            this.gameObject.transform.position = new Vector3(monitorX, transform.position.y, transform.position.z);
        }
        
    }
}
