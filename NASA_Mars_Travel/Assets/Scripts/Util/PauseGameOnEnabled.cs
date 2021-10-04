using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Util
{
    public class PauseGameOnEnabled : MonoBehaviour
    {
        public ShipMovmentManager m_ShipManager;
        private void OnEnable()
        {
            if (m_ShipManager)
                m_ShipManager.PauseGame();
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}