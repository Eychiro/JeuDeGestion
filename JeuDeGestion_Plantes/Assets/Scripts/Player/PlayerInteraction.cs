using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 4f;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PerformRaycastInteraction();
        }
    }

    private void PerformRaycastInteraction()
    {
        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * interactDistance, Color.green, 2f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactDistance))
        {
            // On cherche l'interface sur l'objet touché (ou son parent)
            IInteractible interactible = hit.collider.GetComponentInParent<IInteractible>();

            if (interactible != null)
            {
                interactible.Interact(); // Exécute l'interaction trouvée
            }
        }
    }
}