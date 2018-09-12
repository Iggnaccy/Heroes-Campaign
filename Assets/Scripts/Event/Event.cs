using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event {

    private bool shouldResetProbability = false;
    private double initialProbability;

	public double probability   { get; private set; }
	public double gain          { get; private set; }
    public Popup popup          { get; private set; }
    private EventArgs eventArgs;
    public event EventHandler<EventArgs> callback;

    public Event(double _probability, double _gain/*, Popup _popup*/, EventArgs args,  EventHandler<EventArgs> handler) {
        probability = _probability;
        initialProbability = probability;
        gain = _gain;
        //popup = _popup;
        eventArgs = args;
        callback += handler;
    }

    public bool ShouldFire(double d)
    {
        return d >= probability;
    }

    public void Run()
    {
        if (shouldResetProbability)
            probability = initialProbability;
        callback(this, eventArgs);
    }

    public void Update()
    {
        probability += gain * TimeManager.DeltaTime;
    }
}
