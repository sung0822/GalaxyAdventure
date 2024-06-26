using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeamMember
{
    TeamType Team { get; }
}

public enum TeamType
{
    Ally,
    Enemy
}