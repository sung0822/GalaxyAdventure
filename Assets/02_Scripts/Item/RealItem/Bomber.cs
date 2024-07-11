using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour, ITeamMember
{
    Transform spawnPoint;
    GameObject missile;
    
    public float moveSpd = 10;
    bool isDropped = false;
    public TeamType teamType { get { return _teamType; } set { _teamType = value; } }
    private TeamType _teamType;

    void Start()
    {
        spawnPoint = GameObject.FindWithTag("ALL_SPAWNPOINTS_GROUP").transform.Find("BomberSpawnPoint");
        transform.position = spawnPoint.position;
        missile = Resources.Load<GameObject>("Bullets/missile");
        Destroy(gameObject, 10.0f);
    }

    private void Update()
    {
        transform.Translate(0, 0, 1 * moveSpd * Time.deltaTime);

        if (transform.position.z >= 0 && !isDropped)
        {
            Missile missile = Instantiate(this.missile).GetComponent<Missile>();
            isDropped = true;
        }
    }




}
