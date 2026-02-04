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

        // le "~" permet d'ignorer le layer en question, donc "Ignore Raycast"
        int layerMaskToIgnore = ~LayerMask.GetMask("Ignore Raycast");

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactDistance, layerMaskToIgnore))
        {
            // On cherche l'interface sur l'objet touché (ou son parent
            Debug.Log("J'ai touché : " + hit.collider.name);
            IInteractible interactible = hit.collider.GetComponentInParent<IInteractible>();

            if (interactible != null)
            {
                interactible.Interact(); // Exécute l'interaction trouvée
            }
        }
    }
}