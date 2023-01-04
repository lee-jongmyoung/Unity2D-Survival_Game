using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [SerializeField] Sprite empty;
    [SerializeField] Sprite fill;

    public bool _isAvailable;

    public bool IsAvailable 
    { 
        get { return _isAvailable; }
        set
        {
            _isAvailable = value;

            if (value)
                gameObject.GetComponent<Image>().sprite = fill;
            else
                gameObject.GetComponent<Image>().sprite = empty;

        }
    }


}
