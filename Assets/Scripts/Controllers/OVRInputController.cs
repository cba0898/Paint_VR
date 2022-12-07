using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputController : MonoBehaviour
{
    /// <summary>
    /// The root GameObject that represents the Oculus Touch for Quest 2 Controller model (Left).
    /// </summary>
    public GameObject m_modelOculusTouchQuest2LeftController;

    /// <summary>
    /// The root GameObject that represents the Oculus Touch for Quest 2 Controller model (Right).
    /// </summary>
    public GameObject m_modelOculusTouchQuest2RightController;

    public OVRInput.Controller m_controller;
    private Animator m_animator;
    // Update is called once per frame
    void Update()
    {
        if (m_animator != null)
        {
            m_animator.SetFloat("button 1", OVRInput.Get(OVRInput.Button.One, m_controller) ? 1.0f : 0.0f);
            m_animator.SetFloat("button 2", OVRInput.Get(OVRInput.Button.Two, m_controller) ? 1.0f
                 : 0.0f);
            m_animator.SetFloat("button 3", OVRInput.Get(OVRInput.Button.Start, m_controller) ? 1.0f : 0.0f);

            m_animator.SetFloat("Joy X", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).x);
            m_animator.SetFloat("Joy Y", OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_controller).y);

            m_animator.SetFloat("Trigger", OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller));
            m_animator.SetFloat("±×¸³", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller));
        }
    }
}
