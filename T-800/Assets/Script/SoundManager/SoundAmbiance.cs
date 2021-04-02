using UnityEngine;
using UnityEngine.Events;

public class SoundAmbiance : MonoBehaviour
{
    [System.Serializable]
    private class SoundAmbianceEvent
    {
        public UnityEvent m_AmbianceSound = new UnityEvent();
        public UnityEvent m_SoundWaterDrop = new UnityEvent();
    }
    [SerializeField]
    private SoundAmbianceEvent m_AmbianceSoundEvent = new SoundAmbianceEvent();

    private float m_Timer = 0;
    [SerializeField]
    private float m_TimerMax = 3F;

    [SerializeField]
    private int m_ValidateRandom = 0;

    private int m_Random = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_AmbianceSoundEvent.m_AmbianceSound.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Timer <= m_TimerMax)
        {
            m_Timer += Time.deltaTime;
        }
        else
        {
            m_Timer = 0;
            m_Random = Random.Range(0, 100);
        }

        if(m_Random >= m_ValidateRandom)
        {
            m_AmbianceSoundEvent.m_SoundWaterDrop.Invoke();
            m_Random = 0;
        }
    }
}
