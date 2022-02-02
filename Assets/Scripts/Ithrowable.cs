using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    Rigidbody2D rd { get;  set; }
     void EnableCollider2D();
    //Transform spawn { get; set; }

}
