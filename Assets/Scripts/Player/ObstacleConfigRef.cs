using UnityEngine;

public class ObstacleConfigRef : MonoBehaviour
{
    [Tooltip("Trascina qui lo ScriptableObject dell'ostacolo (deve implementare IObstacleConfig, direttamente o tramite adapter).")]
    [SerializeField] private ScriptableObject obstacleConfig;

    private IObstacleConfig config;

    public int Damage => config != null ? config.Damage : 1;
    public DamageSource Source => config != null ? config.Source : DamageSource.Unknown;

    private void Awake()
    {
        config = obstacleConfig as IObstacleConfig;
        if (obstacleConfig != null && config == null)
        {
            Debug.LogWarning(
                "ObstacleConfigRef: lo ScriptableObject assegnato non implementa IObstacleConfig. Uso fallback damage=1.",
                this
            );
        }
    }
}