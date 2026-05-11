using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel; // Necessário para escutar eventos brutos

public class GlobalListener : MonoBehaviour
{
    void OnEnable()
    {
        // Escuta qualquer evento de entrada de qualquer dispositivo
        InputSystem.onEvent += OnInputEvent;
    }

    void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        // Se não for um evento de mudança de estado, ignora
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        // Varre todos os controles do dispositivo que enviou o sinal
        foreach (var control in device.allControls)
        {
            // Verifica se o valor do controle mudou significativamente
            float value = control.ReadValueFromEventAsObject(eventPtr).ToString() == "0" ? 0 : 1;

            // Para analógicos, precisamos de mais precisão que 0 ou 1
            if (control is UnityEngine.InputSystem.Controls.AxisControl axis)
            {
                float axisValue = axis.ReadValueFromEvent(eventPtr);
                if (Mathf.Abs(axisValue) > 0.1f) // Deadzone básica
                {
                    Debug.Log($"<color=orange>EIXO DETECTADO:</color> Path: {control.path} | Valor: {axisValue}");
                }
            }
            else if (control is UnityEngine.InputSystem.Controls.ButtonControl btn)
            {
                if (btn.ReadValueFromEvent(eventPtr) >= 0.5f)
                {
                    Debug.Log($"<color=cyan>BOTÃO DETECTADO:</color> Path: {control.path}");
                }
            }
        }
    }
}
