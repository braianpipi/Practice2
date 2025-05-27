using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public void Cerrar()
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_CLOSE, 02f);
    }
}
