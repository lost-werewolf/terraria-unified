using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements;

public partial class UICharacterListItem
{
	// Copy-pasted from UICharacterCreation. Should this stuff be migrated to some shared code class?
	private static string GetHexText(Color pendingColor) => "#" + pendingColor.Hex3().ToUpper();

	private string GetPlayerTemplateValues()
	{
		string text = JsonConvert.SerializeObject(new Dictionary<string, object> {
			{ "version", 1 },
			{ "hairStyle", _data.Player.hair },
			{ "clothingStyle", _data.Player.skinVariant },
			{ "voiceStyle", _data.Player.voiceVariant },
			{ "voicePitch", _data.Player.voicePitchOffset },
			{ "hairColor", GetHexText(_data.Player.hairColor) },
			{ "eyeColor", GetHexText(_data.Player.eyeColor) },
			{ "skinColor", GetHexText(_data.Player.skinColor) },
			{ "shirtColor", GetHexText(_data.Player.shirtColor) },
			{ "underShirtColor", GetHexText(_data.Player.underShirtColor) },
			{ "pantsColor", GetHexText(_data.Player.pantsColor) },
			{ "shoeColor", GetHexText(_data.Player.shoeColor) }
		}, new JsonSerializerSettings {
			TypeNameHandling = TypeNameHandling.Auto,
			MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
			Formatting = Formatting.Indented
		});

		PlayerInput.PrettyPrintProfiles(ref text);
		return text;
	}

	private void CopyPlayerTemplateMouseOver(UIMouseEvent evt, UIElement listeningElement)
	{
		_buttonLabel.SetText(Language.GetTextValue("UI.CopyPlayerToClipboard"));
	}

	private void CopyPlayerTemplateButtonClick(UIMouseEvent evt, UIElement listeningElement)
	{
		SoundEngine.PlaySound(12);
		Platform.Get<IClipboard>().Value = GetPlayerTemplateValues();
	}
}
