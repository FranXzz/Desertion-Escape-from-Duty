using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuOpciones : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider; // creo referencia del slider

    private List<Resolution> uniqueResolutions = new List<Resolution>(); // Lista de resoluciones únicas


    void Start()
    {

        // Obtener la resolución actual de la pantalla
        Resolution currentResolution = Screen.currentResolution;


        // Obtener resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;
        HashSet<string> resolutionOptionsSet = new HashSet<string>();  // Usamos un HashSet para asegurarnos de que no haya resoluciones duplicadas


        // Limpiar las opciones actuales del Dropdown
        resolutionDropdown.ClearOptions();


        // Filtrar resoluciones únicas y crear una lista
        foreach (Resolution resolution in resolutions)
        {
            string resolutionString = resolution.width + "x" + resolution.height;

            // Solo agregar resoluciones únicas
            if (!resolutionOptionsSet.Contains(resolutionString))
            {
                resolutionOptionsSet.Add(resolutionString);
                uniqueResolutions.Add(resolution); // Guardar la resolución única
            }
        }

        // Crear la lista de opciones para el Dropdown
        List<string> resolutionOptions = new List<string>();
        foreach (Resolution resolution in uniqueResolutions)
        {
            resolutionOptions.Add(resolution.width + "x" + resolution.height);
        }

        // Añadir las opciones al Dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // Establecer la resolución predeterminada como la resolución actual del usuario
        int currentResolutionIndex = uniqueResolutions.FindIndex(res => res.width == currentResolution.width && res.height == currentResolution.height);
        if (currentResolutionIndex != -1)
        {
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();  // Asegura que el valor del Dropdown se actualice correctamente
        }


        // Cargar la resolución guardada y actualizar el Dropdown
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");

            // Seleccionar la res guardada en el Dropdown
            resolutionDropdown.value = savedResolutionIndex;

            // Aplicar la res guardada
            Resolution selectedResolution = uniqueResolutions[savedResolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    


        // Cargar volumen con playerprefs y actualizar slider
        if (PlayerPrefs.HasKey("Volumen"))
        {
            float volumenGuardado = PlayerPrefs.GetFloat("Volumen");
            audioMixer.SetFloat("Volumen", volumenGuardado);
            volumeSlider.value = volumenGuardado;  // Actualiza el slider con el volumen guardado
        }

    }

    // Metodo para cambiar la res basada en la selección del Dropdown
    public void SetResolution(int resolutionIndex)
    {
        // Guardar el indice seleccionado
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);

        // Obtener la res seleccionada de la lista unica
        if (resolutionIndex >= 0 && resolutionIndex < uniqueResolutions.Count)
        {
            Resolution selectedResolution = uniqueResolutions[resolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }

    public void Cambiarvolumen(float Volumen)
    {
        audioMixer.SetFloat("Volumen", Volumen);
        PlayerPrefs.SetFloat("Volumen", Volumen);  // Guardamos el volumen como playerprefs al cambiarlo
    }

 
}
