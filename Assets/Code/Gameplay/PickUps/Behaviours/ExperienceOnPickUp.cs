using Code.Gameplay.Experience.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.PickUps.Behaviours
{
    [RequireComponent(typeof(PickUp))]
    public class ExperienceOnPickUp : MonoBehaviour
    {
        [SerializeField] private int _experienceAmount;
        
        private PickUp _pickUp;
        private IExperienceService _experienceService;

        [Inject]
        private void Construct(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        private void Awake()
        {
            _pickUp = GetComponent<PickUp>();
        }

        private void OnEnable()
        {
            _pickUp.OnPickUp += HandlePickup;
        }

        private void OnDisable()
        {
            _pickUp.OnPickUp -= HandlePickup;
        }

        private void HandlePickup(GameObject pickUpper)
        {
            _experienceService.AddExperience(_experienceAmount);
        }
    }
}