using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class SendPlayersToGame : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI textBox;
    public GameObject button;
    PhotonView view;
    
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        //text = textObject.GetComponent<TextMeshPro>();
        
        if (PhotonNetwork.IsMasterClient)
        {
            
            textBox.text = "Start";
            
        }
        else
        {
            textBox.text = "Waiting for host...";
            Destroy(button);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void SendingToGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void OnButtonClick()
    {
        
        view.RPC("SendingToGame", RpcTarget.All);
        
        
        
        

    }
    
}
