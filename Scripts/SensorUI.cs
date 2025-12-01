using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SensorUI : MonoBehaviour
{
  public TMP_Text accelerometer_txt;
  public TMP_Text gyroscope_txt;
  public TMP_Text gravity_txt;
  public TMP_Text attitude_txt;
  public TMP_Text linear_acceleration_txt;
  public TMP_Text magnetic_field_txt;
  public TMP_Text light_txt;
  public TMP_Text pressure_txt;
  public TMP_Text proximity_txt;
  public TMP_Text humidity_txt;
  public TMP_Text ambient_temp_txt;
  public TMP_Text step_counter_txt;

  Accelerometer accel;
  UnityEngine.InputSystem.Gyroscope gyro;
  GravitySensor gravity;
  AttitudeSensor attitude;
  LinearAccelerationSensor linear_accel;
  MagneticFieldSensor magnetic;
  LightSensor light_sensor;
  PressureSensor pressure;
  ProximitySensor proximity;
  HumiditySensor humidity;
  AmbientTemperatureSensor ambient_temp;
  StepCounter step_counter;

  void Start()
  {
    accel = Accelerometer.current;
    gyro = UnityEngine.InputSystem.Gyroscope.current;
    gravity = GravitySensor.current;
    attitude = AttitudeSensor.current;
    linear_accel = LinearAccelerationSensor.current;
    magnetic = MagneticFieldSensor.current;
    light_sensor = LightSensor.current;
    pressure = PressureSensor.current;
    proximity = ProximitySensor.current;
    humidity = HumiditySensor.current;
    ambient_temp = AmbientTemperatureSensor.current;
    step_counter = StepCounter.current;

    EnableIfExists(accel);
    EnableIfExists(gyro);
    EnableIfExists(gravity);
    EnableIfExists(attitude);
    EnableIfExists(linear_accel);
    EnableIfExists(magnetic);
    EnableIfExists(light_sensor);
    EnableIfExists(pressure);
    EnableIfExists(proximity);
    EnableIfExists(humidity);
    EnableIfExists(ambient_temp);
    EnableIfExists(step_counter);
  }

  void EnableIfExists(InputDevice input_device)
  {
    if (input_device == null) return;
    if (!input_device.enabled)
      InputSystem.EnableDevice(input_device);
  }

  void Update()
  {
      // ACCELEROMETER
      if (accelerometer_txt != null)
        accelerometer_txt.text = "Accelerometer: " + 
          FormatSensor(accel, () => accel.acceleration.ReadValue().ToString());

      // GYROSCOPE
      if (gyroscope_txt != null)
        gyroscope_txt.text = "Gyroscope: " + 
          FormatSensor(gyro, () => gyro.angularVelocity.ReadValue().ToString());

      // GRAVITY
      if (gravity_txt != null)
        gravity_txt.text = "GravitySensor: " + 
          FormatSensor(gravity, () => gravity.gravity.ReadValue().ToString());

      // ATTITUDE (quaternion -> euler)
      if (attitude_txt != null)
        attitude_txt.text = "AttitudeSensor: " + 
          FormatSensor(attitude, () => FormatQuaternion(attitude.attitude.ReadValue()));

      // LINEAR ACCEL
      if (linear_acceleration_txt != null)
        linear_acceleration_txt.text = "LinearAccelerationSensor: " + 
          FormatSensor(linear_accel, () => linear_accel.acceleration.ReadValue().ToString());

      // MAGNETIC FIELD
      if (magnetic_field_txt != null)
        magnetic_field_txt.text = "MagneticFieldSensor: " + 
          FormatSensor(magnetic, () => magnetic.magneticField.ReadValue().ToString());

      // LIGHT
      if (light_txt != null)
        light_txt.text = "LightSensor: " + 
          FormatSensor(light_sensor, () => FormatFloat(light_sensor.lightLevel.ReadValue()));

      // PRESSURE
      if (pressure_txt != null)
        pressure_txt.text = "Pressure Sensor: " + 
          FormatSensor(pressure, () => FormatFloat(pressure.atmosphericPressure.ReadValue()));

      // PROXIMITY
      if (proximity_txt != null)
        proximity_txt.text = "Proximity Sensor: " +  
          FormatSensor(proximity, () => FormatFloat(proximity.distance.ReadValue()));

      // HUMIDITY
      if (humidity_txt != null)
        humidity_txt.text = "HumiditySensor: " + 
          FormatSensor(humidity, () => FormatFloat(humidity.relativeHumidity.ReadValue()));

      // AMBIENT TEMP
      if (ambient_temp_txt != null)
        ambient_temp_txt.text = "AmbientTemperatureSensor: " + 
          FormatSensor(ambient_temp, () => FormatFloat(ambient_temp.ambientTemperature.ReadValue()));

      // STEP COUNTER (int)
      if (step_counter_txt != null)
        step_counter_txt.text = "Step Counter: " + 
          FormatIntSensor(step_counter, () => (int)step_counter.stepCounter.ReadValue());
  }

  
  string FormatSensor(InputDevice device, System.Func<string> read)
  {
    if (device == null) return "NO DISPONIBLE";
    if (!device.enabled) return "DESHABILITADO";
    try { return read(); }
    catch { return "ERROR"; }
  }

  string FormatIntSensor(InputDevice device, System.Func<int> read)
  {
    if (device == null) return "NO DISPONIBLE";
    if (!device.enabled) return "DESHABILITADO";
    try { return read().ToString(); }
    catch { return "ERROR"; }
  }

  string FormatVector(Vector3 v) => string.Format("({0:F2}, {1:F2}, {2:F2})", v.x, v.y, v.z);
  string FormatQuaternion(Quaternion q) { var e = q.eulerAngles; return string.Format("Euler({0:F1}, {1:F1}, {2:F1})", e.x, e.y, e.z); }
  string FormatFloat(float f) => f.ToString("F2");

  void OnDisable()
  {
    DisableIfExists(accel);
    DisableIfExists(gyro);
    DisableIfExists(gravity);
    DisableIfExists(attitude);
    DisableIfExists(linear_accel);
    DisableIfExists(magnetic);
    DisableIfExists(light_sensor);
    DisableIfExists(pressure);
    DisableIfExists(proximity);
    DisableIfExists(humidity);
    DisableIfExists(ambient_temp);
    DisableIfExists(step_counter);
  }

  void DisableIfExists(InputDevice d)
  {
    if (d == null) return;
    if (d.enabled) InputSystem.DisableDevice(d);
  }
}
