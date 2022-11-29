using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

    public void destroyParentObj() {
        Destroy(transform.parent.gameObject);
    }
}
