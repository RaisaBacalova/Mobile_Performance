using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdPiece : MonoBehaviour
{
    public GameObject GameLogic;
    public GameObject raycastHolder;
    public GameObject player;
    public GameObject pieceBeingHeld;
    public GameObject gravityAttractor;



    public bool holdingPiece = false;
    public float hoverHeight = 0.3f;

    RaycastHit hit;
    public float gravityFactor = 10.0f;
    private Vector3 forceDirection;

    Rigidbody rBody;
    BoxCollider bCol;
    GameLogic gameL;


    // Use this for initialization
    void Start()
    {


    }
    public void grabPiece(GameObject selectedPiece)
    {
        if (selectedPiece.GetComponent<PlayerPiece>().hasBeenPlayed == false)
        {
            pieceBeingHeld = selectedPiece;
            holdingPiece = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        rBody = pieceBeingHeld.GetComponent<Rigidbody>();
        bCol = pieceBeingHeld.GetComponent<BoxCollider>();
        gameL = GameLogic.GetComponent<GameLogic>();

        if (gameL.playerTurn == true)
        {
            if (holdingPiece == true)
            {
                Vector3 forwardDir = raycastHolder.transform.TransformDirection(Vector3.forward) * 100;
                Debug.DrawRay(raycastHolder.transform.position, forwardDir, Color.green);


                if (Physics.Raycast(raycastHolder.transform.position, (forwardDir), out hit))
                {
                    gravityAttractor.transform.position = new Vector3(hit.point.x, hit.point.y + hoverHeight, hit.point.z);


                    rBody.useGravity = false;
                    bCol.enabled = false;

                    //pieceBeingHeld.GetComponent<Rigidbody>().AddForce(gravityAttractor.transform.position - pieceBeingHeld.transform.position);
                   // rBody.AddForce(gravityAttractor.transform.position - pieceBeingHeld.transform.position);
                   rBody.transform.position = Vector3.Lerp(pieceBeingHeld.transform.position, gravityAttractor.transform.position, 1.0f);

                    if (hit.collider.gameObject.tag == "Grid Plate")
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            holdingPiece = false;
                            hit.collider.gameObject.SetActive(false);
                            pieceBeingHeld.GetComponent<PlayerPiece>().hasBeenPlayed = true;

                            //pieceBeingHeld.GetComponent<Rigidbody> ().useGravity = true;
                            //rBody = pieceBeingHeld.GetComponent<Rigidbody>();
                            rBody.useGravity = true;
                            //pieceBeingHeld.GetComponent<BoxCollider> ().enabled = true;
                            bCol.enabled = true;
                            gameL.playerMove(hit.collider.gameObject);

                        }

                    }

                }
            }
        }
    }

}








