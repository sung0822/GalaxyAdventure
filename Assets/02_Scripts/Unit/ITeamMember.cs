using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeamMember
{
    TeamType Team { get; set; }
}

public enum TeamType
{
    ALLY,
    ENEMY
}