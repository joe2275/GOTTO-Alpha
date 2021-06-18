using UnityEngine;

namespace TriggerHandling
{
    public class SubTrigger : MonoBehaviour
    {
        private Trigger mParentTrigger;

        #region Private Methods

        private void OnTriggerEnter(Collider collision)
        {
            mParentTrigger.OnTriggerEnter(collision);
        }

        private void OnTriggerExit(Collider collision)
        {
            mParentTrigger.OnTriggerExit(collision);
        }

        private void Awake()
        {
            mParentTrigger = GetComponentInParent<Trigger>();
        }

        #endregion
    }
}
