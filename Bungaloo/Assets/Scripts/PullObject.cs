using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PullObject : MonoBehaviour
{
    GameObject[] players;
    Rigidbody2D rb;
    Vector3[] mousePos;
    Vector3 dir;

    public float waveLength = 10;
    public float waveForce = 5f;
    float startingWaveForce;
    public float maxWaveForce = 20f;
    public float waveForceIncrement = 1.015f;
    public float waveDist = 5f;
    float startingWaveDist;
    public float waveDistIncrement = 1.05f;
    public float waveLeniency = 1f;
    float startingWaveLeniency;
    public float waveLeniencyIncrement = 1.025f;
    public float maxWaveLeniency = 2.5f;
    public float maxWaveDist = 20f;
    bool spacebarReleased = false;

    bool lockedOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingWaveForce = waveForce;
        startingWaveDist = waveDist;
        startingWaveLeniency = waveLeniency;
    }

    // Update is called once per frame/*
    /*
    void FixedUpdate()
    {
        for(int i = 0; i > players.Length; i++){
            mousePos[i].z = 10;
            mousePos[i] = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast towards whatever collider the mouse is aiming towards
            RaycastHit2D hit = Physics2D.Raycast(players[i].transform.position, (mousePos[i] - players[i].transform.position).normalized, Mathf.Infinity);
            Debug.DrawRay(players[i].transform.position, new Vector3(hit.point.x, hit.point.y, 0) - players[i].transform.position, Color.green);

            Debug.Log(hit.point);
            Debug.Log(transform.position);
            // Pulls object towards player
        // if(Input.GetKey(KeyCode.Space) && transform.position.x < hit.point.x + waveLeniency && transform.position.x > hit.point.x - waveLeniency 
        // && transform.position.y < hit.point.y + waveLeniency && transform.position.y > hit.point.y - waveLeniency || Input.GetKey(KeyCode.Space) && lockedOn){
            if(Input.GetKey(KeyCode.Space) && hit.collider.gameObject == gameObject || Input.GetKey(KeyCode.Space) && lockedOn){
                rb.AddForce((players.transform.position - transform.position).normalized * waveForce);
                player.GetComponent<Rigidbody2D>().AddForce((transform.position - player.transform.position).normalized * waveForce);

                if(waveForce >= maxWaveForce){
                    waveForce = Mathf.Clamp(waveForce, startingWaveForce, maxWaveForce);
                }
                /*
                if(waveLeniency >= maxWaveLeniency){
                    waveLeniency = Mathf.Clamp(waveLeniency, startingWaveLeniency, maxWaveLeniency);
                }
                
                if(waveDist >= maxWaveDist){
                    waveDist = Mathf.Clamp(waveDist, startingWaveDist, maxWaveDist);
                }

                lockedOn = true;
            } 
            
            if(Input.GetKey(KeyCode.Space)){
                waveForce *= waveForceIncrement;
            // waveDist *= waveDistIncrement;
            // waveLeniency *= waveLeniencyIncrement;
            } else {
                waveForce = startingWaveForce;
            //  waveDist = startingWaveDist;
            //  waveLeniency = startingWaveLeniency;
            }

            if(spacebarReleased && transform.position.x > player.transform.position.x - 1.5f && transform.position.x < player.transform.position.x + 1.5f 
            && transform.position.y > player.transform.position.y - 1.5f && transform.position.y < player.transform.position.y + 1.5f){
                Vector2 forceDir = (mousePos - player.transform.position);
                float forcePull = 5f;
                rb.AddForce(forceDir * forcePull * 50 * Time.fixedDeltaTime, ForceMode2D.Impulse);
                spacebarReleased = false;
            }
        }
    }

    void Update(){
        if(Input.GetKeyUp(KeyCode.Space)){
            spacebarReleased = true;
            lockedOn = false;
        }

        players = GameObject.FindGameObjectsWithTag("Player");
    }*/
}
