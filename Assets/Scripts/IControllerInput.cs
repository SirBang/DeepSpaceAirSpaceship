// Definition of the IControllerInput interface for handling input events in a Unity game.
// This interface declares events for various types of input actions that can be performed by a player or AI,
// such as moving forward, turning, and strafing. Each event is associated with a specific type of delegate
// that defines the parameters passed with the event. This allows for a flexible input handling system
// where different game components can subscribe to these events and react accordingly.

public delegate void InputEvent();
// A delegate type for input events that do not require additional data.

public delegate void InputEventFloat(float value);
// A delegate type for input events that involve a single float value, such as throttle or pitch adjustments.

public delegate void InputEventVector3(float x, float y, float z);
// A delegate type for input events that involve three-dimensional data, useful for actions that require
// complex directional input, such as turning in 3D space.

public interface IControllerInput
{
    // Event triggered for forward movement, with the float value indicating the intensity or speed.
    event InputEventFloat ForwardEvent;
    // Event triggered for yaw rotation (turning left or right), with the float value indicating the rotation amount.
    event InputEventFloat YawEvent;
    // Event triggered for pitch rotation (looking up or down), with the float value indicating the rotation amount.
    event InputEventFloat PitchEvent;
    // Event triggered for roll rotation (tilting left or right), with the float value indicating the rotation amount.
    event InputEventFloat RollEvent;
    // Event triggered for side strafing movement, with the float value indicating the intensity or speed.
    event InputEventFloat SideStrafeEvent;
    // Event triggered for vertical strafing movement (moving up or down), with the float value indicating the intensity or speed.
    event InputEventFloat VerticalStrafeEvent;
    // Event triggered for sliding movement, with the float value indicating the intensity or speed.
    event InputEventFloat SlideEvent;
    // Event triggered for turning in 3D space, with the vector components indicating the direction and magnitude of the turn.
    event InputEventVector3 TurnEvent;
}