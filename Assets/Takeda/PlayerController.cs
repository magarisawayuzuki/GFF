using UnityEngine;
/*
/// <summary>
/// PlayerClass
/// </summary>
public class PlayerController : CharaController
{
    protected InputSystem ic = null;
    private Weapon weapon = null;


    
    private void Start()
    {

        weapon = GetComponent<Weapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 武器の記憶の時に_weaponMemoryCountと_memoryCountを増やす
        if (other.gameObject.tag == "WeaponMemory" )
        {
            _weaponMemoryCount++;
            Memory();
        }
        // 普通の記憶は_memoryCountだけ増やす
        else if (other.gameObject.tag == "Memory")
        {
            Memory();
        }
    }


    /// <summary>
    /// _memoryCountを増やす処理
    /// </summary>
    private void Memory()
    {
        if (!_hasMemory)
        {
            _hasMemory = true;
            _memoryCount++;
        }
        else
        {
            _memoryCount++;
        }
    }



    private void OnEnable()
    {
        ic.Enable();
    }

    private void OnDisable()
    {
        ic.Disable();
    }

}
*/