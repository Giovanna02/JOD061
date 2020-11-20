using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public Color color;
    public Material material;
    public GameObject bala;
    public Transform arma;
    float moveSpeed = 0.1f;
    float moveRotation = 10;

    public override void OnStartServer()
    {
        base.OnStartServer();
        CmdChangeColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    public override void OnStartClient()
    {

    }

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = color;
    }


    // Update is called once per frame
    void Update()
    {
        //if (isServer) Debug.Log("Somente no servidor");
        //if (isClient) Debug.Log("Somente no cliente");

        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed);
            transform.Rotate(0, Input.GetAxis("Horizontal") * moveRotation, 0);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject tiro = Instantiate(bala, arma.position, transform.rotation);
            NetworkServer.Spawn(tiro);
        }
    }

    [Command]
    public void CmdChangeColor (Color newColor)  // cliente envia para o servidor
    {
       color = newColor;
       RpcChangeColor (newColor);
    }

    [ClientRpc]
    public void RpcChangeColor (Color newColor)  // servidor envia para os clientes
    {
       material.color = newColor;
    }
}
