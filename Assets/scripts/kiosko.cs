using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class kiosko : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private bool sv;
    [SerializeField]
    public float nrg;
    [SerializeField]
    private float beb;

    public GameObject hst;


    // Start is called before the first frame update
    void Start()
    {
        sv=PhotonNetwork.IsMasterClient;
        if(sv){
        hst=GameObject.Find("Host");
        photonView.RPC("kioskoRPC", RpcTarget.All, hst.GetComponent<host>().nrgINI,hst.GetComponent<host>().bebINI);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


[PunRPC]
void kioskoRPC(float n, float b)
{
   nrg=n;
   beb=b;
}


}
