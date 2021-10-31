using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum Accion { Observar = 1, Seguir, ObservarSeguir };

    [SerializeField] private float velocidad = 3.0f;
    [SerializeField] private Accion accion;
    [SerializeField] private float distanciaPlayer = 3.0f;
    

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }

    private void Comportamiento()
    {
        Vector3 playerDirection = GetPlayerDirection();

        switch (accion)
        {
            case Accion.Observar:
                LookAtPlayer(playerDirection);
                break;
            case Accion.Seguir:
                MoveTowards(playerDirection);
                break;
            case Accion.ObservarSeguir:
                MoveTowards(playerDirection);
                LookAtPlayer(playerDirection);
                break;
        }
    }

    private void MoveTowards(Vector3 newDirection)
    {
        if (newDirection.magnitude > distanciaPlayer)
            transform.position += velocidad * newDirection.normalized * Time.deltaTime;
    }

    private void LookAtPlayer(Vector3 newDirection)
    {
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, velocidad / 2 * Time.deltaTime);
    }

    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }

}
