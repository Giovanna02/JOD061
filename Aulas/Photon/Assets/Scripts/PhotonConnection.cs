using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Conectando na nuvem...");

        //Conecta na nuvem Photon 
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.NickName = "Player"; // + Random.Range(0,9999);
        PhotonNetwork.ConnectUsingSettings();

    }

    override public void OnConnectedToMaster()
    {
        Debug.Log("Conectado");

        //Entra em uma sala
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("JOD061", options, TypedLobby.Default);
    }

    override public void OnJoinedRoom()
    {
        Debug.Log("Jogador " + PhotonNetwork.LocalPlayer.NickName + 
        " entrou na sala " + PhotonNetwork.CurrentRoom.Name + "(" + 
        PhotonNetwork.CurrentRoom.PlayerCount + ")");

        //PhotonNetwork.LoadLevel("PhotonScene");

        PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-5f, 5f), 0f, 0f),
        Quaternion.identity, 0);
    }

    override public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Deu ruim ao entrar na sala! Motivo: " + message);
    }

}
