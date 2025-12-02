using UnityEngine;
using UnityEngine.InputSystem;

public class CompassMovement : MonoBehaviour
{
  public float speed = 6;
  public float warrior_adjustment = 90; // Para ajustar la rotación del guerrero

  private MagneticFieldSensor magnetic;

  void Start()
  {
    magnetic = MagneticFieldSensor.current;

    if (magnetic != null)
      InputSystem.EnableDevice(magnetic);
  }

  void Update()
  {
    if (magnetic == null || !magnetic.enabled) return;

    // Leemos el campo magnético
    Vector3 magnetic_field = magnetic.magneticField.ReadValue();

    // Proyección al plano
    Vector3 flat_north = new Vector3(-magnetic_field.x, 0, -magnetic_field.z).normalized;

    Quaternion final_rotation = Quaternion.LookRotation(flat_north);

    // Ajuste manual del guerrero
    final_rotation *= Quaternion.Euler(0, warrior_adjustment, 0);

    // Rotación suave
    transform.rotation = Quaternion.Slerp(transform.rotation, final_rotation, Time.deltaTime * speed);
  }
}