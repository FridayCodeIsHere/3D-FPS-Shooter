using UnityEngine;
using TMPro;
using Zenject;

public class GUIInstaller : MonoInstaller
{
    [SerializeField] private TextMeshProUGUI _interactText;
    public override void InstallBindings()
    {
        Container.Bind<TextMeshProUGUI>().FromInstance(_interactText).AsSingle();
    }
}