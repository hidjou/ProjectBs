using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Player.grounded || Player.wallSliding) AudioManager.instance.PlaySound2D("jump");
            player.OnJumpInputDown();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            player.OnJumpInputUp();
        }
    }
}
