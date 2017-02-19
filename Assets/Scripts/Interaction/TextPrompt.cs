﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPrompt : MonoBehaviour {

	public string[] imagePaths;
	public string[] stringKeys;
	public float overridePromptDuration = -1;
	public bool noTimer;

	Sprite[] images;
	string[] stringPrompts;

	protected int promptIndex;

	protected virtual void Awake() {
		stringPrompts = new string[stringKeys.Length];
		for (int i = 0; i < stringKeys.Length; i++) {
			stringPrompts[i] = PromptStrings.prompts [stringKeys[i]];
		}
		if (imagePaths.Length > 0) {
			images = new Sprite[imagePaths.Length];
			for (int i = 0; i < imagePaths.Length; i++) {
				if (imagePaths [i] != "")
					images [i] = Resources.Load<Sprite> ("Sprites/TextPrompts/" + imagePaths [i]);
				else
					images [i] = null;
			}
		} else
			images = new Sprite[stringKeys.Length];
		
		promptIndex = -1;
	}

	public void BeginPrompt() {
		if (promptIndex != -1)
			return;
		SendPromptEvent (false);
	}

	public void ContinuePrompt() {
		if (promptIndex < stringPrompts.Length - 1) {
			SendPromptEvent (true);
		} else {
			PromptController.textPrompted (this, null);
		}
	}

	public virtual void CheckToContinue() {}

	public void SendPromptEvent(bool overrideEvent) {
		if (overrideEvent)
			PromptController.activePrompt = null;
		promptIndex++;
		TextPromptEventArgs textE = new TextPromptEventArgs ();
		textE.text = stringPrompts [promptIndex];
		textE.image = images [promptIndex];
		textE.overrideDuration = overridePromptDuration;
		PromptController.textPrompted (this, textE);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Entity> () == PlayerController.activeCharacter) {
			BeginPrompt ();
		}
	}
}