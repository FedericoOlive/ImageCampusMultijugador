using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class parametros : MonoBehaviourPunCallbacks
{

    public GameObject nickT;
    Transform cam;

    //  
    public float energia;
    public GameObject nick;


    public Text tnick;

    public int vida;                            // VARIABLE DONMDE GUARDAMOS LA VIDA DEL PERSONAJE
    public int puntos;
    public GameObject came;
    public Rigidbody rg;
    public bool mio;
    public float tiempo;


    public float x;
    public float y;
    // SISTEMA DE CHEQUEO DE DAÑOS
    // detecto colisiones





    void OnTriggerStay(Collider col)
    {

        if (col.tag == "interact")
        {
            Debug.Log("" + col.GetComponent<kiosko>().nrg);

            if (Input.GetKeyDown(KeyCode.E) && energia < 90)

            {
                if (col.GetComponent<kiosko>().nrg > 10)
                    energia += 70;





                tnick.text = photonView.Owner.NickName + " " + energia;

                photonView.RPC("nrg", RpcTarget.Others, energia);


            }

        }




    }







    void OnCollisionEnter(Collision col)
    {


        // PRUEBA OFFLINE

        if (col.gameObject.tag == "bala")
        {
            Destroy(gameObject);

        }







        // chequeo que soy remoto o local
        if (!photonView.IsMine)
        {
            // si soy remoto chequeo que sea una bala
            if (col.gameObject.tag == "bala")
            {
                // me resto vida
                vida -= 1;
                // le envio mensaje a mi local o dueño que tengo un punto menos de vida
                // VIA RPC a la funcion  RPC VIDA
                photonView.RPC("Vida", RpcTarget.All, vida);
            }
        }
    }






    void Start()
    {

        // Nick sobre la cabeza siguiendo a la camara en roation Y
        //////////////////////////////
        // pelo aleatorio         
        ////////////////////////////

        //  PhotonView photonView = PhotonView.Get(this);  

        if (!photonView.IsMine)
        {
            mio = false;
        }
        else
        {
            mio = true;
            tnick.text = photonView.Owner.NickName + " " + energia;

        }

        if (!mio)
        {
            GetComponent<ThirdPersonUserControl>().enabled = false;
            GetComponent<ThirdPersonCharacter>().enabled = false;

            GetComponent<DriveVehicle>().enabled = false;  // AUTO DRIVE

            //  GetComponent<inst>().enabled=false;
            // GetComponent<CameraWork>().enabled=false;

        }
        else
        {

            rg = GetComponent<Rigidbody>();
            came = GameObject.Find("Camara"); // BUSCO CAMARA EN ESCENA
            came.GetComponent<PlayerCamera>().target = this.transform;  // BUSCO CAMARA Y LE DIGO QUE ME SIGA SI SOY LOCAL

            // int p=Random.Range(0,pelo.Length);
            // pelo[p].SetActive(true);

            //nickT.GetComponent<TMPro.TextMeshPro>().text=""+PhotonNetwork.NickName;
            //   nick=""+PhotonNetwork.NickName;

            photonView.RPC("nickRP", RpcTarget.All, "hola"); // NICKNAME NUESTROS A TODOS
                                                             // ENVIAMOS AL HOST NUESTRA VIDA Y NUESTROS PUNTOS COMO INICIO::::::::::::::::::::::
        }
        Debug.Log("" + PhotonNetwork.NickName);

    }

    // Update is called once per frame
    void Update()
    {
        tnick.transform.position = new Vector2(transform.position.x, transform.position.y + 470);

        if (mio)
        {

            float velo = GetComponent<Rigidbody>().velocity.magnitude;



            // chequeamos cada x s, y enviamos a remotos x RPC (serna chequeados por el host)...
            if (velo > 2 && energia >= 0)
            {

                // cada 1 seg envio por RPC 

                energia -= 0.1f;
                tnick.text = photonView.Owner.NickName + " " + energia;
                // mensaje RPC nueva energia o cantidad de energia que resto, 

                if (energia <= 0)
                    sinEnergia();
            }

            // CHECK HOST

        }






        //        if(PhotonView.isMine)



        //:::::::::::::::::::::::::::::::::: 



        // SISTEMA DE DISPARO (SE EJECUTA EN USUARIO LOCAL) :::::::::::::

        // 1. 

        //   if(Input.GetKey(KeyCode.Z))
        // {




        //}




        // 2.




        ///////////////////////////////////////////////////////////////////



    }





    void host()
    {

        // CHEQUEO DATOS DE LOS USUARIOS SI SOY HOST
    }


    public void sinEnergia()
    {
        //
    }




    [PunRPC]
    void nickRP(string n)
    {
        nickT.GetComponent<TMPro.TextMeshPro>().text = n;
    }



    // funcion que recibe el daño en compu o jugador LOCAL
    [PunRPC]
    void Vida(int v)
    {

        // CHEQUEO QUE SOY LOCAL ACTUALIZO MI VIDA
        if (photonView.IsMine)
        {
            vida = v;////////////// ACTUALIZACION ..
        }
    }


    [PunRPC]
    void nrg(float n)
    {
        if (!photonView.IsMine)
            energia = n;
    }



    void peloRP(int pe)
    {
        //                pelo[pe].SetActive(true);
    }




}
