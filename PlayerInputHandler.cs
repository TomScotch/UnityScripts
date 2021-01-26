﻿using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {
    public float lookSensitivity = 1f;
    public float webglLookSensitivityMultiplier = 0.25f;
    public float triggerAxisThreshold = 0.4f;
    public bool invertYAxis = false;
    public bool invertXAxis = false;

    PlayerCharacterController m_PlayerCharacterController;
    bool m_FireInputWasHeld;

    private void Start () {
        m_PlayerCharacterController = GetComponent<PlayerCharacterController> ();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate () {
        m_FireInputWasHeld = GetFireInputHeld ();
    }

    public bool CanProcessInput () {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    public Vector3 GetMoveInput () {
        if (CanProcessInput ()) {
            Vector3 move = new Vector3 (Input.GetAxisRaw (GameConstants.k_AxisNameHorizontal), 0f, Input.GetAxisRaw (GameConstants.k_AxisNameVertical));
            move = Vector3.ClampMagnitude (move, 1);
            return move;
        }

        return Vector3.zero;
    }

    public float GetLookInputsHorizontal () {
        return GetMouseOrStickLookAxis (GameConstants.k_MouseAxisNameHorizontal, GameConstants.k_AxisNameJoystickLookHorizontal);
    }

    public float GetLookInputsVertical () {
        return GetMouseOrStickLookAxis (GameConstants.k_MouseAxisNameVertical, GameConstants.k_AxisNameJoystickLookVertical);
    }

    public bool GetJumpInputDown () {
        if (CanProcessInput ()) {
            return Input.GetButtonDown (GameConstants.k_ButtonNameJump);
        }

        return false;
    }

    public bool GetJumpInputHeld () {
        if (CanProcessInput ()) {
            return Input.GetButton (GameConstants.k_ButtonNameJump);
        }

        return false;
    }

    public bool GetFireInputDown () {
        return GetFireInputHeld () && !m_FireInputWasHeld;
    }

    public bool GetFireInputReleased () {
        return !GetFireInputHeld () && m_FireInputWasHeld;
    }

    public bool GetFireInputHeld () {
        if (CanProcessInput ()) {
            bool isGamepad = Input.GetAxis (GameConstants.k_ButtonNameGamepadFire) != 0f;
            if (isGamepad) {
                return Input.GetAxis (GameConstants.k_ButtonNameGamepadFire) >= triggerAxisThreshold;
            } else {
                return Input.GetButton (GameConstants.k_ButtonNameFire);
            }
        }

        return false;
    }

    public bool GetAimInputHeld () {
        if (CanProcessInput ()) {
            bool isGamepad = Input.GetAxis (GameConstants.k_ButtonNameGamepadAim) != 0f;
            bool i = isGamepad ? (Input.GetAxis (GameConstants.k_ButtonNameGamepadAim) > 0f) : Input.GetButton (GameConstants.k_ButtonNameAim);
            return i;
        }

        return false;
    }

    public bool GetSprintInputHeld () {
        if (CanProcessInput ()) {
            return Input.GetButton (GameConstants.k_ButtonNameSprint);
        }

        return false;
    }

    public bool GetCrouchInputDown () {
        if (CanProcessInput ()) {
            return Input.GetButtonDown (GameConstants.k_ButtonNameCrouch);
        }

        return false;
    }

    public bool GetFlashlightInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetButtonDown(GameConstants.k_ButtonNameFlashlight);
        }
        return false;
    }
    public bool GetCrouchInputReleased () {
        if (CanProcessInput ()) {
            return Input.GetButtonUp (GameConstants.k_ButtonNameCrouch);
        }

        return false;
    }

    public int GetSwitchWeaponInput () {
        if (CanProcessInput ()) {

            bool isGamepad = Input.GetAxis (GameConstants.k_ButtonNameGamepadSwitchWeapon) != 0f;
            string axisName = isGamepad ? GameConstants.k_ButtonNameGamepadSwitchWeapon : GameConstants.k_ButtonNameSwitchWeapon;

            if (Input.GetAxis (axisName) > 0f)
                return -1;
            else if (Input.GetAxis (axisName) < 0f)
                return 1;
            else if (Input.GetAxis (GameConstants.k_ButtonNameNextWeapon) > 0f)
                return -1;
            else if (Input.GetAxis (GameConstants.k_ButtonNameNextWeapon) < 0f)
                return 1;
        }
        return 0;
    }

    public int GetSelectWeaponInput () {
        if (CanProcessInput ()) {
            if (Input.GetKeyDown (KeyCode.Alpha1))
                return 1;
            else if (Input.GetKeyDown (KeyCode.Alpha2))
                return 2;
            else if (Input.GetKeyDown (KeyCode.Alpha3))
                return 3;
            else if (Input.GetKeyDown (KeyCode.Alpha4))
                return 4;
            else if (Input.GetKeyDown (KeyCode.Alpha5))
                return 5;
            else if (Input.GetKeyDown (KeyCode.Alpha6))
                return 6;
            else
                return 0;
        }
        return 0;
    }

    float GetMouseOrStickLookAxis (string mouseInputName, string stickInputName) {
        if (CanProcessInput ()) {

            bool isGamepad = Input.GetAxis (stickInputName) != 0f;
            float i = isGamepad ? Input.GetAxis (stickInputName) : Input.GetAxisRaw (mouseInputName);

            i *= lookSensitivity;
            if (isGamepad) {
                i *= Time.deltaTime;
            } else {
                i *= 0.01f;
#if UNITY_WEBGL
                i *= webglLookSensitivityMultiplier;
#endif
            }
            return i;
        }
        return 0f;
    }
}