using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrapScript : MonoBehaviour
{
    public PlayerController player;
    public Material trapMaterial;
    public Material damageMaterial;
    public Material defaultMaterial;

    private enum TrapState 
    { 
        Idle, 
        Triggered, 
        TriggeredAndExited
    }
    private TrapState trapState = TrapState.Idle;
    private const float ATTACK_DELAY = 1;
    private const float RESET_DELAY = 5;
    private const float MATERIAL_DELAY = 0.2f;
    private const int TRAP_DAMAGE = 25;

    private void OnTriggerStay(Collider other)
    {
        if (trapState == TrapState.Idle)
        {
            trapState = TrapState.Triggered;
            if (trapMaterial != null)
            {
                GetComponent<MeshRenderer>().material = trapMaterial;
            }
            Invoke("Attack", ATTACK_DELAY);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(trapState == TrapState.Triggered)
        {
            trapState = TrapState.TriggeredAndExited;
        }
    }

    private void Attack()
    {
        if (damageMaterial != null)
        {
            GetComponent<MeshRenderer>().material = damageMaterial;
        }
        if (trapState == TrapState.Triggered && player != null)
        {
            player.TakeDamage(TRAP_DAMAGE);
        }
        //ввел задержку так как незаметно моргание красным цветом
        Invoke("ResetMaterial", MATERIAL_DELAY);
        Invoke("ResetTrapState", RESET_DELAY);
    }

    private void ResetMaterial()
    {
        if (trapMaterial != null)
        {
            GetComponent<MeshRenderer>().material = trapMaterial;
        }
    }

    private void ResetTrapState()
    {
        if (defaultMaterial != null)
        {
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        trapState = TrapState.Idle;
    }
}
