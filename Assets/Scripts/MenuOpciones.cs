using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;

public class MenuOpciones : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider; // creo referencia del slider

    private List<Resolution> uniqueResolutions = new List<Resolution>(); // Lista de resoluciones únicas

    public TMP_Dropdown fpsDropdown; // Dropdown para el limitador de FPS
    private List<int> fpsOptions = new List<int> { 60, 144, 240, 360, 0 }; // Opciones de FPS

    void Start()
    {
        SetupFPSOptions();
        LoadFPSLimit(); // 
        LoadResolutionSettings();
        LoadVolumeSettings();
    }

   

    void LoadResolutionSettings()
    {
        Resolution currentResolution = Screen.currentResolution;
        Resolution[] resolutions = Screen.resolutions;
        HashSet<string> resolutionOptionsSet = new HashSet<string>();
        resolutionDropdown.ClearOptions();

        foreach (Resolution resolution in resolutions)
        {
            string resolutionString = resolution.width + "x" + resolution.height;
            if (!resolutionOptionsSet.Contains(resolutionString))
            {
                resolutionOptionsSet.Add(resolutionString);
                uniqueResolutions.Add(resolution);
            }
        }

        List<string> resolutionOptions = new List<string>();
        foreach (Resolution resolution in uniqueResolutions)
        {
            resolutionOptions.Add(resolution.width + "x" + resolution.height);
        }

        resolutionDropdown.AddOptions(resolutionOptions);

        int currentResolutionIndex = uniqueResolutions.FindIndex(res => res.width == currentResolution.width && res.height == currentResolution.height);
        if (currentResolutionIndex != -1)
        {
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = savedResolutionIndex;
            Resolution selectedResolution = uniqueResolutions[savedResolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }

    void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("Volumen"))
        {
            float volumenGuardado = PlayerPrefs.GetFloat("Volumen");
            audioMixer.SetFloat("Volumen", volumenGuardado);
            volumeSlider.value = volumenGuardado;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        if (resolutionIndex >= 0 && resolutionIndex < uniqueResolutions.Count)
        {
            Resolution selectedResolution = uniqueResolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }

    public void Cambiarvolumen(float Volumen)
    {
        audioMixer.SetFloat("Volumen", Volumen);
        PlayerPrefs.SetFloat("Volumen", Volumen);
    }


    // Método para inicializar el selector de FPS
    private void SetupFPSOptions()
    {
        // Llenar el Dropdown de FPS con las opciones
        fpsDropdown.ClearOptions();
        List<string> fpsLabels = fpsOptions.ConvertAll(fps => fps + " FPS");
        fpsDropdown.AddOptions(fpsLabels);

        // Cargar FPS guardado o establecer un valor por defecto
        int savedFPS = PlayerPrefs.GetInt("FPSLimit", 240);
        Application.targetFrameRate = savedFPS;

        // Actualizar el Dropdown al valor guardado
        int index = fpsOptions.IndexOf(savedFPS);
        if (index != -1)
        {
            fpsDropdown.value = index;
        }

        // Asignar evento al cambiar la opción del Dropdown
        fpsDropdown.onValueChanged.AddListener(SetFPSLimit);
    }

    public void SetFPSLimit(int index)
    {
        int selectedFPS = fpsOptions[index];
        QualitySettings.vSyncCount = 0;  // Desactiva VSync para que el límite de FPS funcione correctamente.
        Application.targetFrameRate = selectedFPS;

        PlayerPrefs.SetInt("FPSLimit", selectedFPS);
    }

    void LoadFPSLimit()
    {
        if (PlayerPrefs.HasKey("FPSLimit"))
        {
            int savedFPS = PlayerPrefs.GetInt("FPSLimit");
            QualitySettings.vSyncCount = 0;  // Asegurar que VSync está desactivado
            Application.targetFrameRate = savedFPS;

            // Convertir la lista fpsOptions a un array para usar Array.IndexOf
            int[] fpsArray = fpsOptions.ToArray();

            // Asegurar que el Dropdown muestre la opción correcta
            int dropdownIndex = System.Array.IndexOf(fpsArray, savedFPS);
            if (dropdownIndex != -1)
            {
                fpsDropdown.value = dropdownIndex;
                fpsDropdown.RefreshShownValue();
            }
        }
    }


}