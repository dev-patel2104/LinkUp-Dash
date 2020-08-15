using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFollow : MonoBehaviour
{
    [Header("GameObject Player")]
    public Transform Player;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float smoothFactor;

    [SerializeField]
    private Vector3 botOffset;

    // Start is called before the first frame update
    void Start()
    {
        botOffset = transform.position - Player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = Player.position + botOffset;
        transform.position = Vector3.Slerp(transform.position, newPos , smoothFactor);
    }
}
