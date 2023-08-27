using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buildable : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject buildUI;
    [SerializeField] private TMP_Text foundationAmountText;
    [SerializeField] private TMP_Text wallsAmountText;
    [SerializeField] private TMP_Text detailsAmountText;
    [SerializeField] private TMP_Text roofAmountText;
    [SerializeField] private Inventory inventory;

    [Header("Tweaks")]
    [SerializeField] private int foundationAmount = 50;
    [SerializeField] private int wallsAmount = 75;
    [SerializeField] private int roofAmount = 25;
    [SerializeField] private int detailsAmount = 50;

    [Header("Materials")]
    [SerializeField] private Material foundationMaterial;
    [SerializeField] private Material wallsMaterial;
    [SerializeField] private Material detailsMaterial;
    [SerializeField] private Material lightsMaterial;
    [SerializeField] private Material roofMaterial;
    private Dictionary<string, Material> partMaterialPairs;

    [Header("Effects")]
    [SerializeField] private ParticleSystem poof;
    [SerializeField] private ShakeTransform shakeTransform;
    [SerializeField] private ShakeTransformEventData buildShakeSettings;

    private bool woodSubtracted;
    private bool stoneSubtracted;
    private bool claySubtracted;
    private bool redClaySubtracted;

    private void Start()
    {
        inventory.UpdateCurrentResourcesUI();
        UpdateResourceUI(foundationMaterial.name, foundationAmount, foundationAmountText);
        UpdateResourceUI(wallsMaterial.name, wallsAmount, wallsAmountText);
        UpdateResourceUI(detailsMaterial.name, detailsAmount, detailsAmountText);
        UpdateResourceUI(roofMaterial.name, roofAmount, roofAmountText);

        partMaterialPairs = new Dictionary<string, Material>
        {
            { "Foundation", foundationMaterial },
            { "Walls", wallsMaterial },
            { "Details", detailsMaterial },
            { "Lights", lightsMaterial },
            { "Roof", roofMaterial }
        };

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_DitherThreshold", 0.25f);
            }
        }

        buildUI.SetActive(false);
    }

    private void Update()
    {
        if (BuildIsDone())
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

                buildUI.SetActive(false);

                CameraController.instance.followTransform = null;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildUI.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (BuildIsDone())
        {
            return;
        }

        buildUI.SetActive(true);

        CameraController.instance.followTransform = transform;
    }

    public void BuildPart(string part)
    {
        Renderer[] partRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in partRenderers)
        {
            foreach (Material material in renderer.materials)
            {
                if (!partMaterialPairs.ContainsKey(part))
                {
                    return;
                }

                if (material.GetColor("_BaseColor") == partMaterialPairs[part].GetColor("_BaseColor"))
                {
                    if (material.name == "Wood (Instance)" && material.GetColor("_BaseColor") == foundationMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentWood < foundationAmount)
                        {
                            return;
                        }
                        else if (inventory.currentWood >= foundationAmount && !woodSubtracted)
                        {
                            inventory.currentWood -= foundationAmount;
                            woodSubtracted = true;
                        }
                    }
                    if (material.name == "Wood (Instance)" && material.GetColor("_BaseColor") == wallsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentWood < wallsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentWood >= wallsAmount && !woodSubtracted)
                        {
                            inventory.currentWood -= wallsAmount;
                            woodSubtracted = true;
                        }
                    }
                    if (material.name == "Wood (Instance)" && material.GetColor("_BaseColor") == detailsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentWood < detailsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentWood >= detailsAmount && !woodSubtracted)
                        {
                            inventory.currentWood -= detailsAmount;
                            woodSubtracted = true;
                        }
                    }
                    if (material.name == "Wood (Instance)" && material.GetColor("_BaseColor") == roofMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentWood < roofAmount)
                        {
                            return;
                        }
                        else if (inventory.currentWood >= roofAmount && !woodSubtracted)
                        {
                            inventory.currentWood -= roofAmount;
                            woodSubtracted = true;
                        }
                    }
                    if (material.name == "Stone (Instance)" && material.GetColor("_BaseColor") == foundationMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentStone < foundationAmount)
                        {
                            return;
                        }
                        else if (inventory.currentStone >= foundationAmount && !stoneSubtracted)
                        {
                            inventory.currentStone -= foundationAmount;
                            stoneSubtracted = true;
                        }
                    }
                    if (material.name == "Stone (Instance)" && material.GetColor("_BaseColor") == wallsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentStone < wallsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentStone >= wallsAmount && !stoneSubtracted)
                        {
                            inventory.currentStone -= wallsAmount;
                            stoneSubtracted = true;
                        }
                    }
                    if (material.name == "Stone (Instance)" && material.GetColor("_BaseColor") == detailsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentStone < detailsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentStone >= detailsAmount && !stoneSubtracted)
                        {
                            inventory.currentStone -= detailsAmount;
                            stoneSubtracted = true;
                        }
                    }
                    if (material.name == "Stone (Instance)" && material.GetColor("_BaseColor") == roofMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentStone < roofAmount)
                        {
                            return;
                        }
                        else if (inventory.currentStone >= roofAmount && !stoneSubtracted)
                        {
                            inventory.currentStone -= roofAmount;
                            stoneSubtracted = true;
                        }
                    }
                    if (material.name == "Clay (Instance)" && material.GetColor("_BaseColor") == foundationMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < foundationAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= foundationAmount && !claySubtracted)
                        {
                            inventory.currentClay -= foundationAmount;
                            claySubtracted = true;
                        }
                    }
                    if (material.name == "Clay (Instance)" && material.GetColor("_BaseColor") == wallsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < wallsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= wallsAmount && !claySubtracted)
                        {
                            inventory.currentClay -= wallsAmount;
                            claySubtracted = true;
                        }
                    }
                    if (material.name == "Clay (Instance)" && material.GetColor("_BaseColor") == detailsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < detailsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= detailsAmount && !claySubtracted)
                        {
                            inventory.currentClay -= detailsAmount;
                            claySubtracted = true;
                        }
                    }
                    if (material.name == "Clay (Instance)" && material.GetColor("_BaseColor") == roofMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < roofAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= roofAmount && !claySubtracted)
                        {
                            inventory.currentClay -= roofAmount;
                            claySubtracted = true;
                        }
                    }
                    if (material.name == "Red Clay (Instance)" && material.GetColor("_BaseColor") == foundationMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < foundationAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= foundationAmount && !redClaySubtracted)
                        {
                            inventory.currentClay -= foundationAmount;
                            redClaySubtracted = true;
                        }
                    }
                    if (material.name == "Red Clay (Instance)" && material.GetColor("_BaseColor") == wallsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < wallsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= wallsAmount && !redClaySubtracted)
                        {
                            inventory.currentClay -= wallsAmount;
                            redClaySubtracted = true;
                        }
                    }
                    if (material.name == "Red Clay (Instance)" && material.GetColor("_BaseColor") == detailsMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < detailsAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= detailsAmount && !redClaySubtracted)
                        {
                            inventory.currentClay -= detailsAmount;
                            redClaySubtracted = true;
                        }
                    }
                    if (material.name == "Red Clay (Instance)" && material.GetColor("_BaseColor") == roofMaterial.GetColor("_BaseColor"))
                    {
                        if (inventory.currentClay < roofAmount)
                        {
                            return;
                        }
                        else if (inventory.currentClay >= roofAmount && !redClaySubtracted)
                        {
                            inventory.currentClay -= roofAmount;
                            redClaySubtracted = true;
                        }
                    }

                    inventory.UpdateCurrentResourcesUI();
                    UpdateResourceUI(foundationMaterial.name, foundationAmount, foundationAmountText);
                    UpdateResourceUI(wallsMaterial.name, wallsAmount, wallsAmountText);
                    UpdateResourceUI(detailsMaterial.name, detailsAmount, detailsAmountText);
                    UpdateResourceUI(roofMaterial.name, roofAmount, roofAmountText);

                    material.SetFloat("_DitherThreshold", 1f);

                    if (partMaterialPairs[part] != lightsMaterial)
                    {
                        poof.GetComponent<Renderer>().material = partMaterialPairs[part];
                        poof.Play();

                        shakeTransform.AddShakeEvent(buildShakeSettings);

                        AudioManager.instance.UpdateSettings();
                        switch (partMaterialPairs[part].name)
                        {
                            case "Wood":
                                AudioManager.instance.Play("WoodPlace");
                                break;
                            case "Stone":
                                AudioManager.instance.Play("StonePlace");
                                break;
                            case "Clay":
                                AudioManager.instance.Play("ClayPlace");
                                break;
                            case "Red Clay":
                                AudioManager.instance.Play("ClayPlace");
                                break;
                        }
                    }
                }
            }
        }
    }

    private bool BuildIsDone()
    {
        // Checks if all dither threshold values are equal to one, meaning the build is done.

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in renderer.materials)
            {
                if (material.GetFloat("_DitherThreshold") != 1f)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void UpdateResourceUI(string materialName, int amount, TMP_Text amountText)
    {
        switch (materialName)
        {
            case "Wood":
                amountText.text = "Wood: " + inventory.currentWood + "/" + amount;
                break;
            case "Stone":
                amountText.text = "Stone: " + inventory.currentStone + "/" + amount;
                break;
            case "Clay":
                amountText.text = "Clay: " + inventory.currentClay + "/" + amount;
                break;
            case "Red Clay":
                amountText.text = "Clay: " + inventory.currentClay + "/" + amount;
                break;
        }
    }
}
