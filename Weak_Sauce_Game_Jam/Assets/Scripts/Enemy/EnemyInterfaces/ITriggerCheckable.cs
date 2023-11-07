using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAgrroed { get; set; }
    bool IsStriking { get; set; }

    void SetAggroStatus(bool isAgrroed);
    void SetIsStrikingBool(bool isStriking);

}
