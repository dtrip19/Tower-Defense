using UnityEngine;

public class OnTriggerEnterMessageSender : MonoBehaviour
{
    [SerializeField] RootBehavior target;

    private void OnTriggerEnter(Collider other)
    {
        target.SendMessage("OnTriggerEnter", other);
    }
}
