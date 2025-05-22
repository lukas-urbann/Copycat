using BachelorProject.Audio;
using BachelorProject.Events;
using UnityEngine;
using UnityEngine.Events;

namespace BachelorProject.Interactable
{
    //id smeru
    public enum RotationDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    /// <summary>
    /// Pouziva se pro sochy v levelu Outside.
    /// Dava moznost otocit sochu o 90 stupnu a zkontrolovat zda je otoceni spravne.
    /// Interakce se zaklada na tride UsableInteractable.
    /// </summary>
    public class StatueInteractable : UseableInteractable
    {
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private int currentFacingIndex = 0;
        [SerializeField] private AudioCall statueRotateAudio;
        [SerializeField] private BoolEvent onRotateStatue;
        [SerializeField] private RotationDirection actualRotation = RotationDirection.North;
        [SerializeField] private RotationDirection correctRotation = RotationDirection.North;
        private Quaternion targetRotation;
        private bool isRotating = false;

        private readonly Vector3[] facingDirections = new Vector3[]
        {
            new Vector3(0, 0, 0), // North
            new Vector3(0, 90, 0), // East
            new Vector3(0, 180, 0), // South
            new Vector3(0, 270, 0) // West
        };

        private void Start()
        {
            float currentYRotation = transform.eulerAngles.y;
            currentFacingIndex = Mathf.RoundToInt(currentYRotation / 90f) % 4;
            targetRotation = transform.rotation;
        }

        private void OnEnable()
        {
            UnityEventOnInteract = new UnityEvent();
            UnityEventOnInteract.AddListener(RotateStatue);
        }
        
        //Otaci sochou
        private void Update()
        {
            if (isRotating)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    transform.rotation = targetRotation;
                    isRotating = false;
                    SetActualRotation();
                }
            }
        }

        //Vola event kde kontroluje zda je socha otocena spravne
        private void SetActualRotation()
        {
            actualRotation = (RotationDirection)currentFacingIndex;
            onRotateStatue?.Execute(actualRotation == correctRotation);
        }

        //Zavola se pri interakci s sochou
        private void RotateStatue()
        {
            currentFacingIndex = (currentFacingIndex + 1) % 4;
            targetRotation = Quaternion.Euler(facingDirections[currentFacingIndex]);
            isRotating = true;
            statueRotateAudio?.Play();
        }
    }
}
