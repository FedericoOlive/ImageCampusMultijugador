using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class host : MonoBehaviourPunCallbacks
{

    // array palabras reservadas

    // Start is called before the first frame update
    public bool sv;
    
    public GameObject hstBTN;
   
    public float nrgINI;
    public float bebINI;

   public Transform[] spwns;
   public string[] interact;


    void Start()
    {
        sv=PhotonNetwork.IsMasterClient;


            if(sv){
                     Debug.Log("HS");
                spawns();
    
            } else{
                    Debug.Log("HH");
                Destroy(this);
                }
       //Sistema host dedicado , check//

        // opcion universal
       // chequear IP y puerto.


    
    

    }

    // Update is called once per frame
    void Update()
    {
        


    }


void spawns(){




 GameObject kiosko =PhotonNetwork.Instantiate(interact[0], spwns[0].transform.position, Quaternion.identity, 0);
 hstBTN.SetActive(true);




}





}
