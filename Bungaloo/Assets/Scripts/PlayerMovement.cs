using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    PhotonView view;
    Vector3 forceDir;
    public float moveSpeed;
    public float slowDownSpeed = 0.05f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 1f;
    public float maxTorque = 1f;
    public float forcePush = 5f;
    public float forcePushIncrement = 1.015f;
    public float forcePushMultiplier = 2f;
    public float maxForcePush = 10f;
    public float jumpCooldownTime = 1000f;
    float startingCooldownTime;
    float startingForcePush;
    public float jumpLeniency = 0.2f;
    bool forcinGrounded = false;
    bool canJump = true;
    bool cooldownActive = false;
    Vector3 mousePos;

    bool spacebarReleased_F = false;
    bool spacebarReleased_P = false;
    bool canSlowDown = false;
    bool lockedOn = false;
    GameObject lockedOnObj;

    public bool slowDown = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        startingForcePush = forcePush;
        startingCooldownTime = jumpCooldownTime;

       /* if(!view.IsMine){
            Destroy(rb);
        }*/
    }

    void FixedUpdate()
    {
        if(!view.IsMine) return;
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.AddTorque(horizontalInput * -rotationSpeed * Time.fixedDeltaTime);

            if(Input.GetKey(KeyCode.A)){
                rb.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime);
            } 

            if(Input.GetKey(KeyCode.D)){
                rb.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime);
            } 

            // Whenever player starts moving again after a jump, allows horizontal movement to be slowed down again
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                forcinGrounded = false;
            }

            // Makes sure horizontal movement isn't slippy and slidy
            if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && canSlowDown && !forcinGrounded && slowDown){
                rb.AddForce(new Vector2(-rb.velocity.x, 0) * slowDownSpeed * Time.fixedDeltaTime);
            }

            mousePos.z = 10;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxTorque, maxTorque);
            PullObjectTemp();
            Force();
        }
    void Update(){
        if(!view.IsMine) return;
            if(Input.GetKeyUp(KeyCode.Space) && !cooldownActive || Input.GetKeyUp(KeyCode.Space) && jumpCooldownTime < jumpLeniency){
                lockedOn = false;
                spacebarReleased_F = true;
                spacebarReleased_P = true;
            }

            if(cooldownActive){
                jumpCooldownTime -= Time.deltaTime;

                if(jumpCooldownTime < 0){
                    Debug.Log(cooldownActive);
                    jumpCooldownTime = startingCooldownTime;
                    cooldownActive = false;
                    Debug.Log(cooldownActive);
                }
            }
            forceDir = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0);
            forceDir = forceDir.normalized;  
        }

    void Force(){
        if(!view.IsMine) return;
            if(Input.GetKey(KeyCode.Space)){
                //rb.AddForce(forceDir *  forcePull * Time.fixedDeltaTime);
                forcePush *= forcePushIncrement;

                if(forcePush >= maxForcePush){
                    forcePush = Mathf.Clamp(forcePush, startingForcePush, maxForcePush);
                }
            }
            
            // Force given to player
            if(spacebarReleased_F && !cooldownActive){
                // makes it so jumping again midair is actually feasible
                rb.velocity = Vector3.zero;

                rb.AddForce(-forceDir * forcePush * forcePushMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
                forcePush = startingForcePush;
                forcinGrounded = true;
                canJump = false;
                cooldownActive = true;
                spacebarReleased_F = false;
                //StartCoroutine("JumpForceTimer");
            }
        }
    [PunRPC]
    void PullObjectTemp(){
            // Raycast towards whatever collider the mouse is aiming towards
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, Mathf.Infinity);
            Debug.DrawRay(transform.position, new Vector3(hit.point.x, hit.point.y, 0) - transform.position, Color.green);
            Vector2 playerPos = transform.position;

            // Pulls object towards player
        // if(Input.GetKey(KeyCode.Space) && transform.position.x < hit.point.x + waveLeniency && transform.position.x > hit.point.x - waveLeniency 
        // && transform.position.y < hit.point.y + waveLeniency && transform.position.y > hit.point.y - waveLeniency || Input.GetKey(KeyCode.Space) && lockedOn){
            if(Input.GetKey(KeyCode.Space) && hit.collider.gameObject.CompareTag("Physics Object") || Input.GetKey(KeyCode.Space) && lockedOn){
                if(lockedOn == false){
                    lockedOnObj = hit.collider.gameObject;
                }
                if(lockedOnObj.layer == 6) {
                    view.RPC("GetPulled", RpcTarget.Others);
                } else {
                    Debug.Log(lockedOnObj.name);
                    lockedOnObj.GetComponent<Rigidbody2D>().AddForce((new Vector2(transform.position.x, transform.position.y) - new Vector2(lockedOnObj.transform.position.x, lockedOnObj.transform.position.y)).normalized * forcePush * 5);
                }
                GetComponent<Rigidbody2D>().AddForce((new Vector2(lockedOnObj.transform.position.x, lockedOnObj.transform.position.y) - playerPos).normalized * forcePush * 5);
                /*
                if(waveForce >= maxWaveForce){
                    waveForce = Mathf.Clamp(waveForce, startingWaveForce, maxWaveForce);
                }
                /*
                if(waveLeniency >= maxWaveLeniency){
                    waveLeniency = Mathf.Clamp(waveLeniency, startingWaveLeniency, maxWaveLeniency);
                }
                
                if(waveDist >= maxWaveDist){
                    waveDist = Mathf.Clamp(waveDist, startingWaveDist, maxWaveDist);
                }*/

                lockedOn = true;
            }
            /*
            if(Input.GetKey(KeyCode.Space)){
                waveForce *= waveForceIncrement;
            // waveDist *= waveDistIncrement;
            // waveLeniency *= waveLeniencyIncrement;
            } else {
                waveForce = startingWaveForce;
            //  waveDist = startingWaveDist;
            //  waveLeniency = startingWaveLeniency;
            }*/

            if(spacebarReleased_P && lockedOnObj.transform.position.x > transform.position.x - 1.5f && lockedOnObj.transform.position.x < transform.position.x + 1.5f 
            && lockedOnObj.transform.position.y > transform.position.y - 1.5f && lockedOnObj.transform.position.y < transform.position.y + 1.5f){
                Debug.Log("Kinda workin");
                if(lockedOnObj.layer == 6) {
                    Debug.Log("HUGUGH");
                    view.RPC("GetPushed", RpcTarget.Others);
                } else {
                    Debug.Log("EVEN FUCKING WORSE");
                    lockedOnObj.GetComponent<Rigidbody2D>().AddForce(forceDir * forcePush * 200 * Time.fixedDeltaTime, ForceMode2D.Impulse);
                }
                spacebarReleased_P = false;
            } 
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") && view.IsMine){
            canSlowDown = true;
        } else {
            canSlowDown = false;
        }
    }

    [PunRPC]
    void GetPulled(){
        Debug.Log("NANI");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos - transform.position).normalized, Mathf.Infinity);
        GetComponent<Rigidbody2D>().AddForce((new Vector2(hit.transform.position.x, hit.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * forcePush * 5);
    }

    [PunRPC]
    void GetPushed(){
        GetComponent<Rigidbody2D>().AddForce(forceDir * forcePush * 20000 * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
/*
    IEnumerator JumpForceTimer(){
        yield return new WaitForSeconds(jumpCooldownTime);
        canJump = true;
    }*/
}
