using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance == this)
            {
                return;
            }
            Debug.Log("Particle duplication");
            Destroy(this.gameObject);
        }
    }
    public static ParticleManager Get() { return instance; }

    public GameObject basicParticle { get { return _basicParticle; } }
    [SerializeField] private GameObject _basicParticle;

    public GameObject unitExplodingParticle { get { return _unitExplodingParticle; } }
    [SerializeField] private GameObject _unitExplodingParticle;

    public GameObject eagleDieParticle { get { return _eagleDieParticle; } }
    [SerializeField] private GameObject _eagleDieParticle;
    private void Start()
    {
        _eagleDieParticle = Resources.Load<GameObject>("Particles/BloodSplash");
        _basicParticle = Resources.Load<GameObject>("Particles/Basic_Explode");
        _unitExplodingParticle = Resources.Load<GameObject>("Particles/SmallExplosion");
    }


    public GameObject CreateParticle(GameObject particlePrefab, Transform parent)
    {
        if (particlePrefab != null)
        {
            GameObject particle = Instantiate<GameObject>(particlePrefab, parent);
            HandleScaleMode(particle);
            return particle;
        }
        return null;
    }
    public GameObject CreateParticle(GameObject particlePrefab, Vector3 pos, Quaternion quaternion)
    {     
        if (particlePrefab != null)
        {
            GameObject particle = GameObject.Instantiate<GameObject>(particlePrefab, pos, Quaternion.Euler(0, 0, 0));
            particle.transform.SetParent(MainManager.instance.mainStage.transform);
            HandleScaleMode(particle);

            return particle;
        }
        return null;
    }
    public GameObject CreateParticle(GameObject particlePrefab, Vector3 pos, Quaternion quaternion, Vector3 size)
    {
        if (particlePrefab != null)
        {
            GameObject particle = GameObject.Instantiate<GameObject>(particlePrefab, pos, Quaternion.Euler(0, 0, 0));
            particle.transform.SetParent(MainManager.instance.mainStage.transform);
            HandleScaleMode(particle);

            return particle;
        }
        return null;
    }

    private void HandleScaleMode(GameObject particle)
    {
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        main.scalingMode = ParticleSystemScalingMode.Local;

    }
}